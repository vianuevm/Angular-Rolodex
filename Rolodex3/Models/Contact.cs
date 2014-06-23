using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Rolodex3.Models
{

    public class Contact
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string homePhone { get; set; }
        public string workPhone { get; set; }
        public string cellPhone { get; set; }
        public string img { get; set; }


        public Contact(
           string firstName = "",
           string lastName = "",
           string address = "",
           string city = "",
           string state = "",
           string image = "anon.jpg",
           string zip = "",
           string homePhone = "",
           string workPhone = "",
           string cellPhone = "")
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.city = city;
            this.state = state;
            this.img = image;
            this.zip = zip;
            this.homePhone = homePhone;
            this.workPhone = workPhone;
            this.cellPhone = cellPhone;

        }

        public Contact()
            : this("")
        {

        }

    }

    //public class Contact
    //{
    //    private static int currentID = 0;
    //    private int thisID = currentID++;

    //    public int id { get { return thisID; } }

    //    public string firstName { get; set; }
    //    public string lastName { get; set; }
    //    public string address { get; set; }
    //    public string city { get; set; }
    //    public string state { get; set; }
    //    public string img { get; set; }
    //    public string zip { get; set; }
    //    public string homePhone { get; set; }
    //    public string workPhone { get; set; }
    //    public string cellPhone { get; set; }


    //    }
    //}

}