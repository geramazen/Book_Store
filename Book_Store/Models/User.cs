using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "الاسم")]
        public string Name { get; set; }
        
        [Required]
        [Display(Name = "البريد الإلكتروني")]
        public string EMAIL { get; set; }

        [Required]
        [Display(Name = "رقم الهاتف")]
        public string Phone { get; set; }

        [Required]
        [Display(Name = "العنوان")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "اسم المستخدم")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }
    }
}