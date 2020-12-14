using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LBMSservices.Models
{
    public class Book
    {
        public int Rfid { get; set; }

        public string name { get; set; }

        public string author { get; set; }

        public string ebook { get; set; }

        public string genre { get; set; }

        public int userID { get; set; }

        public string publishedOn { get; set; }

        public string publicationCompany { get; set; }

    }
}