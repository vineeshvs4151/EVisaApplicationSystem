using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class Application
    {
        public int ApplicationId { get; set; }
        public int Userid { get; set; }
        public string VisaType { get; set; }
        public string ApplicationDate { get; set; }
        public string Country { get; set; }
        public string PassportNumber { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
        public string MedicalCertificate { get; set; }
        public string VisaDuration { get; set; }
        public User User { get; set; }
    }
}
