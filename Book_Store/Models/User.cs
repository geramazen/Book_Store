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
        [Display(Name = "اسم المستخدم")]
        public string Name { get; set; }
        [Display(Name = "البريد الإلكتروني")]
        public string EMAIL { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "كلمة المرور")]
        public string Password { get; set; }
    }
}