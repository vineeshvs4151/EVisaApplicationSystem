using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EVisaApplicationSystem.Models
{
    public class About
    {
       
        [Display(Name = "Id")]
        public int Id { get; set; }

        
        [Display(Name = "First name")]
        [DataType(DataType.Text)]
        
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Display(Name = "Title")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Image")]
        [DataType(DataType.ImageUrl)]
        public string Image { get; set; }

    }

}
