using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class Visatype
    {
        [Display(Name = "Visa value")]
        public string VisaTypeValue { get; set; }

        [Display(Name ="Visa name")]
        public string VisaTypeName { get; set; }

        public string VisaDuration { get; set; }
    }
}