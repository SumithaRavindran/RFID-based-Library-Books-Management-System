/*
 * Library Books Management System 
 * Author: Jordan Overbo
 * Created on: 12/10/2020
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
    public class MapController : ApiController
    {
        //Database Connection
        public MySqlCommand executeSQL()
        {
            string cs = @"server=localhost;port=3306;userid=root;password=JordanRootPassword;database=LibNET";
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;
            return cmd;
        }

        // Gets a list of locatiosn from our DB and returns a list of Location objects containing relevant library location
        // information.
        [HttpGet]
        [Route("map/locations/")]
        public IHttpActionResult AllLocations()
        {
            var cmd = executeSQL();
            cmd.CommandText = $"SELECT name, address, longitude, latitude FROM locations;";
            MySqlDataReader reader = cmd.ExecuteReader();

            List<Location> result = new List<Location>();

            while (reader.Read())
            {
                Location temp = new Location();
                temp.name = reader.GetString(0);
                temp.address = reader.GetString(1);
                temp.lon = reader.GetDouble(2);
                temp.lat = reader.GetDouble(3);
                result.Add(temp);
            }


            return Ok(result);
        }
    }
}
