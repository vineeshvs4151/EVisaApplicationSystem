using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class Contact
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string MessageStatus { get; set; }
        public string RepliedMessage { get; set; }
    }
}