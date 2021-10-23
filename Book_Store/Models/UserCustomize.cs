using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    [MetadataType(typeof(UserMetaData))]
    public partial class User
    {
        [Required]
        [Display(Name ="Confirm Email")]
        [Compare("EMAIL", ErrorMessage = "Email is not matching")]
        public string ConfEmail { get; set; }
        [Compare("Password", ErrorMessage = "Password is not matching")]
        [Required]
        [Display(Name = "Confirm Password")]
        public string ConfPassword { get; set; }
    }

    public class UserMetaData
    {
        public int ID { get; set; }
        [Display(Name = "User Name")]
        public string Name { get; set; }
        [Required]
        //[RegularExpression(@"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?", ErrorMessage = "Email is Not Correct")]
        public string EMAIL { get; set; }
        [Required]
        //[RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password is Not Strong")]
        public string Password { get; set; }
        public string role { get; set; }
        public byte[] image { get; set; }
    }
}