/*
 * Library Books Management System 
 * Author: Sumitha Ravindran
 * Created on: 12/03/2020
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LBMSservices.Models;
using MySql.Data.MySqlClient;

namespace LBMSservices.Controllers
{
    public class RegistrationController : ApiController
    {
        //Database Connection
        public MySqlCommand executeSQL()
        {
            string cs = @"server=localhost;userid=root;password=******;database=LBMSdb";
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            return cmd;
        }

        //EmployeeRegistration

        [HttpPost]
        [Route("register")]
        public string EmployeeRegistration(string Fname, string Lname, string email, string gender, string phNum, string Username, string password)
        {
            try
            {
                if ((Fname == null) || (Lname == null) || (email == null) || (phNum == null) || (Username == null) || (password == null))
                {
                    return ("Please fill all the required parameters in the form!");
                }
                else
                {
                    var cmd = executeSQL();
                    cmd.CommandText = $"INSERT INTO Employees(Fname, Lname, email, gender, phoneNum, userName, pass) VALUES('{Fname}','{Lname}','{email}','{gender}','{phNum}','{Username}','{password}')";
                    var rowsAffected = cmd.ExecuteNonQuery();
                    if (rowsAffected == 1)
                    {
                        return "Registered Successfully";
                    }
                    else
                    {
                        return "exception occured";
                    }
                }
                
            }
            
            catch (Exception ex)
            {
                return (ex.Message);
            }     
        }

        //EmployeeLogin

        [HttpGet]
        [Route("login/{Username}/{password}")]
        public string EmployeeLogin(string Username, string password)
        {
            try
            {
                if ((Username == null) || (password == null))
                {
                    return ("Please fill all the required parameters in the form!");
                }
                else
                {
                    var cmd = executeSQL();
                    cmd.CommandText = $"SELECT * FROM Employees WHERE userName='" + Username + "' and pass='" + password + "'";
                    MySqlDataReader reader = cmd.ExecuteReader();
                    var rowsAffected = reader.HasRows;
                    if (rowsAffected == false)
                    {
                        return ("Username doesn't exist in the system, Please register your details!");
                    }
                    else if (rowsAffected == true)
                    {
                        return ("Login successful");
                    }
                    else
                    {
                        throw new Exception("Exception Occured!");
                    }
                }
            }
            catch (Exception ex)
            {
                return (ex.Message);
            }

        }
    }
}
