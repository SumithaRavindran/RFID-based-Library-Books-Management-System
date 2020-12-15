/*
 * Library Books Management System 
 * TCSS559: Project Group 2
 * Author: Sumitha Ravindran
 * Created on: 12/10/2020
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using LBMSservices.Models;
//using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using Newtonsoft.Json.Linq;

namespace LBMSservices.Controllers
{
    //class to get value from the body in updateUser method
    public class put_id
    {
        public int userID { get; set; }
    }

    public class UpsertAndDeleteController : ApiController
    {
        //Database Connection
        public MySqlCommand executeSQL()
        {
            //connection string
            string cs = @"server=localhost;userid=root;password=ravithilagam;database=LibNET";
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            return cmd;
        }


        //FindURL method : Using existing service provider API to get ebook URL by giving book name as an input
        [HttpGet]
        [Route("ebook/{bookName}")]
        public string FindURL(string bookName)
        {
            try
            {
                //null check
                if (bookName == null)
                {
                    return ("Please enter the required parameters in the form of Query string");
                }
                else
                {
                    // identifies the service endpoint for Service Provider bookmeth 
                    var serviceURL = ($"https://bookmeth1.p.rapidapi.com/?q={bookName}&start=0&sort=datedesc");
                    // prepare the HTTP request   
                    System.Net.WebRequest serviceRequest = (System.Net.WebRequest)WebRequest.Create(serviceURL);
                    // identify the method of HTTP request as GET   
                    serviceRequest.Method = "GET";
                    serviceRequest.ContentLength = 0;
                    serviceRequest.ContentType = "plain/text";
                    serviceRequest.Headers.Add("x-rapidapi-key", "1d07f95f3dmsh4a57b630c6101efp10baf0jsne78aed223839");
                    serviceRequest.Headers.Add("x-rapidapi-host", "bookmeth1.p.rapidapi.com");

                    // establish a connection and retrieve a HTTP response message   
                    System.Net.WebResponse serviceResponse = (WebResponse)serviceRequest.GetResponse();
                    // read response data stream   
                    Stream receiveStream = serviceResponse.GetResponseStream();
                    // properly set the encoding as utf-8   
                    Encoding encode = Encoding.GetEncoding("utf-8");
                    // encode the stream using utf-8   
                    StreamReader readStream = new StreamReader(receiveStream, encode, true);
                    // read entire stream and store in serviceResult   
                    string serviceResult = readStream.ReadToEnd();
                    //using JArray
                    JArray jarray = JArray.Parse(serviceResult);

                    //checking the count 
                    if (jarray.Count >= 1)
                    {
                        //getting only the value of the external_link from the object and storing it as a string variable 
                        string URL = jarray[0].SelectToken("_source.external_link").ToString();
                        return URL;
                    }

                    else if (jarray.Count < 1)
                    {
                        //return error message when given book name is not available
                        
                        return  "Book name is incorrect!! Please check your input.";
                    }

                    else
                    {
                        //Handlingexception
                        throw new Exception("Exception Occured");
                    }
                }
            }
            catch (Exception)
            {
                //exception
                throw new Exception("Exception Occured in FindURL");
            }
        }


        //UpsertBookDetails method: To insert record if the input RFID not exists in the table, else the method will update the record which has RFID given in the input
        [HttpPost]
        [Route("UpsertBook")]
        public string upsertBookDetails(int Rfid, string name, string Author, string genre, int userID, string publishedOn, string publicationCompany)
        {
            try
            {
                //null check
                if ((genre == null) || (name == null) || (Author == null) || (publishedOn == null) || (publicationCompany == null))
                {
                    return ("Please fill all the required parameters in the form!");
                }

                else
                {
                    var cmd = executeSQL();

                    
                    // select query to check whether the given RFID exists in the table or not
                    cmd.CommandText = $"SELECT * FROM books WHERE rfid={Rfid}";

                    MySqlDataReader reader = cmd.ExecuteReader();
                    bool AffectedRows = reader.HasRows;
                    reader.Close();

                    
                    if (AffectedRows == false)
                    {
                        //calling FindURL method to get the ebook url by giving book name as an input
                        string URL = FindURL(name);

                        //insert query to add record to the books table
                        cmd.CommandText = $"INSERT INTO LibNET.books (Rfid, name, Author, ebook_url, genre, userID, publishedOn, publicationCompany) VALUES({Rfid},'{name}','{Author}','{URL}','{genre}',{userID},'{publishedOn}','{publicationCompany}')";
                        var rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            return "New Book details added Successfully";
                        }
                        else
                        {
                            return "exception occured while insert";
                        }
                    }
                    else if (AffectedRows == true)
                    {
                        //update the record with the input values
                        cmd.CommandText = $"UPDATE books SET name=" + (char)39 + name + (char)39
                               + "," + "Author =" + (char)39 + Author + (char)39
                               + "," + "Genre =" + (char)39 + genre + (char)39
                               + "," + "UserID =" + userID
                               + "," + "publishedOn =" + (char)39 + publishedOn + (char)39
                               + "," + "publicationCompany =" + (char)39 + publicationCompany + (char)39
                               + "WHERE Rfid=" + Rfid + ";";
                        var rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            return "Book details were updated Successfully";
                        }
                        else
                        {
                            return "exception occured while update";
                        }
                    }

                    else
                    {
                        return "Exception occured in Upsert";
                    }
                }

            }
            //exception handling
            catch (Exception ex)
            {
                return (ex.Message);
            }
        }


        //deleteBookByRfid method: To delete a book record from books table using Rfid
        //delete method
        [HttpDelete]
        [Route("deleteBook/{Rfid}")]
        public IHttpActionResult deleteBookByRfid(int Rfid)
        {
            try
            {
                var cmd = executeSQL();
                //delete query to delete the book with the RFid given in the input
                cmd.CommandText = ("DELETE FROM LibNET.books WHERE Rfid =" + (char)39 + Rfid + (char)39 + ";");
                var rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    //customer header to know the status 
                    response.Headers.Add("Status", "Record for the book with RFID" + Rfid + " has been deleted");
                    response.Content = new StringContent("Book deleted successfully");
                    return ResponseMessage(response);

                }
                //When Rfid doesnt exists in the table, returns response message "Rfid doesn't exists" with custom header
                HttpResponseMessage response_RFIDnotfound = new HttpResponseMessage(HttpStatusCode.OK);
                response_RFIDnotfound.Headers.Add("Status", "Record for the book with RFID " + Rfid + " doesn't exists!");
                response_RFIDnotfound.Content = new StringContent("Rfid doesn't exists");
                return ResponseMessage(response_RFIDnotfound);

            }
            catch (Exception)
            {
                throw new Exception("Exception occured in Delete");

            }
        }


        //updateUser method: Update the user details using RfiD
        //Put method
        [HttpPut]
        [Route("checkout")]
        public IHttpActionResult updateUser(int Rfid, [FromBody] put_id putModel)
        {
            try
            {
                var cmd = executeSQL();
                //updates the record with the input userId
                cmd.CommandText = "UPDATE LibNET.books SET userID =" + putModel.userID + " WHERE Rfid=" + Rfid + ";";
                var rowsAffected = cmd.ExecuteNonQuery();
                
                if (rowsAffected == 1)
                {
                    HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                    //customer header to know the status
                    response.Headers.Add("Status", "User id for " + Rfid + " has been updated");
                    response.Content = new StringContent("Userid updated successfully");
                    return ResponseMessage(response);
                    
                }

                HttpResponseMessage response_RFIDnotfound = new HttpResponseMessage(HttpStatusCode.OK);
                response_RFIDnotfound.Headers.Add("Status", "Record for " + Rfid + " doesn't exists!");
                response_RFIDnotfound.Content = new StringContent("Rfid doesn't exists");
                return ResponseMessage(response_RFIDnotfound);

            }
            //Handling Exception
            catch (Exception)
            {
                throw new Exception("Exception occured in Put method");

            }

        }

        
    }
}
