using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LBMSservices.Models
{
    public class Employee
    {
       
        public string Fname { get; set; }

        public string Lname { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string phoneNum { get; set; }

        public string userName { get; set; }
   
        public string pass { get; set; }
     
        public string ConfirmPass { get; set; }
    }
}