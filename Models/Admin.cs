using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class Admin
    {

        public int ID { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Gender { get; set; }
        public string Dateofbirth { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public string ImagePath { get; set; }
    }
}