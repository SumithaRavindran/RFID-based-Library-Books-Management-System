using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using MySql.Data.MySqlClient;

namespace LBMSservices
{
    /// <summary>
    /// Summary description for DetailsSOAP
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DetailsSOAP : System.Web.Services.WebService
    {

        [WebMethod]
        public XmlDocument GetBooks()
        {
            string cs = @"server=localhost;port=3306;userid=root;password=JordanRootPassword;database=LibNET";
            MySqlConnection con = new MySqlConnection(cs);
            con.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = $"SELECT * FROM books";

            MySqlDataReader reader = cmd.ExecuteReader();

            XmlDocument doc = new XmlDocument();
            XmlNode root = doc.CreateElement("books");
            doc.AppendChild(root);

            while (reader.Read())
            {
                XmlNode bookNode = doc.CreateElement("book");
                root.AppendChild(bookNode);
                XmlNode rfidNode = doc.CreateElement("rfid");
                XmlNode nameNode = doc.CreateElement("name");
                XmlNode authorNode = doc.CreateElement("author");
                XmlNode urlNode = doc.CreateElement("ebook_url");
                XmlNode genreNode = doc.CreateElement("genre");
                XmlNode userIDNode = doc.CreateElement("userID");
                XmlNode dateNode = doc.CreateElement("publishedOn");
                XmlNode companyNode = doc.CreateElement("publishedBy");
                rfidNode.InnerText = reader.GetString(0);
                nameNode.InnerText = reader.GetString(1);
                authorNode.InnerText = reader.GetString(2);
                urlNode.InnerText = reader.GetString(3);
                genreNode.InnerText = reader.GetString(4);
                userIDNode.InnerText = reader.GetString(5);
                dateNode.InnerText = reader.GetString(6);
                companyNode.InnerText = reader.GetString(7);
                bookNode.AppendChild(rfidNode);
                bookNode.AppendChild(nameNode);
                bookNode.AppendChild(authorNode);
                bookNode.AppendChild(urlNode);
                bookNode.AppendChild(genreNode);
                bookNode.AppendChild(userIDNode);
                bookNode.AppendChild(dateNode);
                bookNode.AppendChild(companyNode);
            }

            return doc;
        }
    }
}
