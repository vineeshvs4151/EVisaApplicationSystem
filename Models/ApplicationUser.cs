using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class ApplicationUser
    {
        public Application Application { get; set; }
        public User User { get; set; }
    }
}