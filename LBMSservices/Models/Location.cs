using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LBMSservices.Models
{
    public class Location
    {
        public string name { get; set; }
        
        public string address { get; set; }
        
        public double lat { get; set; }

        public double lon { get; set; }
    }
}