/*
 * Library Books Management System 
 * Author: Jordan Overbo
 * Created on: 12/05/2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LBMSservices.Models;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;

namespace LBMSservices.Controllers
{
    public class DetailsController : ApiController
    {
        //Database Connection
        public MySqlCommand executeSQL()
        {
            string cs = @"server=localhost;port=3306;userid=root;password=useyourpassword;database=LibNET";
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            return cmd;
        }

        // Returns list of distinct genres that occur in our books in the DB.
        [HttpGet]
        [Route("details/genre/")]
        public HttpResponseMessage AllGenres()
        {
            var cmd = executeSQL();
            cmd.CommandText = $"SELECT DISTINCT genre FROM books";
            MySqlDataReader reader = cmd.ExecuteReader();

            List<string> result = new List<string>();

            while (reader.Read())
            {
                result.Add(reader.GetValue(0).ToString());
            }

            var message = this.Request.CreateResponse();

            if (result.Count == 0)
            {
                message.StatusCode = HttpStatusCode.NotFound;
                message.Headers.Add("error", "The books were not found.");
            }
            else
            {
                message.StatusCode = HttpStatusCode.OK;
                message.Headers.Add("total-books", result.Count.ToString());
                message.Content = new StringContent(JsonConvert.SerializeObject(result),
                                            System.Text.Encoding.UTF8, "application/json");
            }

            return message;
        }

        // Returns a list of all available books in our DB.
        [HttpGet]
        [Route("details/available/")]
        public HttpResponseMessage AllAvailable()
        {
            var cmd = executeSQL();
            cmd.CommandText = $"SELECT * FROM books WHERE userID = -1";
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Book> result = new List<Book>();

            while (reader.Read())
            {
                Book temp = new Book();
                temp.Rfid = reader.GetInt32(0);
                temp.name = reader.GetString(1);
                temp.author = reader.GetString(2);
                temp.ebook = reader.GetString(3);
                temp.genre = reader.GetString(4);
                temp.userID = reader.GetInt32(5);
                temp.publishedOn = reader.GetString(6);
                temp.publicationCompany = reader.GetString(7);
                result.Add(temp);
            }

            var message = this.Request.CreateResponse();

            if (result.Count == 0)
            {
                message.StatusCode = HttpStatusCode.NotFound;
                message.Headers.Add("error", "The books were not found.");
            }
            else
            {
                message.StatusCode = HttpStatusCode.OK;
                message.Headers.Add("total-books", result.Count.ToString());
                message.Content = new StringContent(JsonConvert.SerializeObject(result),
                                            System.Text.Encoding.UTF8, "application/json");
            }

            return message;
        }

        // Returns the details of all of our books in the DB.
        [HttpGet]
        [Route("details/")]
        public HttpResponseMessage AllBooks()
        {
            var cmd = executeSQL();
            cmd.CommandText = $"SELECT * FROM books";
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Book> result = new List<Book>();

            while (reader.Read())
            {
                Book temp = new Book();
                temp.Rfid = reader.GetInt32(0);
                temp.name = reader.GetString(1);
                temp.author = reader.GetString(2);
                temp.ebook = reader.GetString(3);
                temp.genre = reader.GetString(4);
                temp.userID = reader.GetInt32(5);
                temp.publishedOn = reader.GetString(6);
                temp.publicationCompany = reader.GetString(7);
                result.Add(temp);
            }

            var message = this.Request.CreateResponse();

            if (result.Count == 0)
            {
                message.StatusCode = HttpStatusCode.NotFound;
                message.Headers.Add("error", "The books were not found.");
            } 
            else
            {
                message.StatusCode = HttpStatusCode.OK;
                message.Headers.Add("total-books", result.Count.ToString());
                message.Content = new StringContent(JsonConvert.SerializeObject(result), 
                                            System.Text.Encoding.UTF8, "application/json");
            }

            return message;
        }

        // Returns a book from the DB matching the RFID if there is one.
        [HttpGet]
        [Route("details/byRfid/{rfid}")]
        public HttpResponseMessage BooksByRfid(string rfid)
        {
            Console.WriteLine(rfid);
            var cmd = executeSQL();
            cmd.CommandText = $"SELECT * FROM books where Rfid = '" + rfid + "';";
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Book> result = new List<Book>();

            while (reader.Read())
            {
                Book temp = new Book();
                temp.Rfid = reader.GetInt32(0);
                temp.name = reader.GetString(1);
                temp.author = reader.GetString(2);
                temp.ebook = reader.GetString(3);
                temp.genre = reader.GetString(4);
                temp.userID = reader.GetInt32(5);
                temp.publishedOn = reader.GetString(6);
                temp.publicationCompany = reader.GetString(7);
                result.Add(temp);
            }

            var message = this.Request.CreateResponse();

            if (result.Count == 0)
            {
                message.StatusCode = HttpStatusCode.NotFound;
                message.Headers.Add("error", "The books were not found.");
            }
            else
            {
                message.StatusCode = HttpStatusCode.OK;
                message.Headers.Add("total-books", result.Count.ToString());
                message.Content = new StringContent(JsonConvert.SerializeObject(result),
                                            System.Text.Encoding.UTF8, "application/json");
            }

            return message;
        }

        // Returns a list of books matching the given genre.
        // Always will return at least one book since the web page
        // only displays genres that show up in our DB.
        [HttpGet]
        [Route("details/byGenre/{genre}")]
        public HttpResponseMessage BooksByGenre(string genre)
        {
            var cmd = executeSQL();
            if (genre.Equals("all"))
            {
                cmd.CommandText = $"SELECT * FROM books";
            }
            else
            {
                cmd.CommandText = $"SELECT * FROM books where genre ='" + genre +"'";
            }
            
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Book> result = new List<Book>();

            while (reader.Read())
            {
                Book temp = new Book();
                temp.Rfid = reader.GetInt32(0);
                temp.name = reader.GetString(1);
                temp.author = reader.GetString(2);
                temp.ebook = reader.GetString(3);
                temp.genre = reader.GetString(4);
                temp.userID = reader.GetInt32(5);
                temp.publishedOn = reader.GetString(6);
                temp.publicationCompany = reader.GetString(7);
                result.Add(temp);
            }

            var message = this.Request.CreateResponse();

            if (result.Count == 0)
            {
                message.StatusCode = HttpStatusCode.NotFound;
                message.Headers.Add("error", "The books were not found.");
            }
            else
            {
                message.StatusCode = HttpStatusCode.OK;
                message.Headers.Add("total-books", result.Count.ToString());
                message.Content = new StringContent(JsonConvert.SerializeObject(result),
                                            System.Text.Encoding.UTF8, "application/json");
            }

            return message;
        }
    }
}
