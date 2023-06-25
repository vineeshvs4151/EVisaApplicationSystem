using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class Country
    {
        [Display(Name = "Country value")]
        public string CountryValue { get; set; }

        [Display(Name = "Country")]
        public string CountryName { get; set; }
        

    }
}