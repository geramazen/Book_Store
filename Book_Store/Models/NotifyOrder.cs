using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class NotifyOrder
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "الإسم")]
        public string Name { get; set; }
        [Display(Name = "اسم الكتاب")]
        public string BookName { get; set; }
        [Display(Name = ")الواتساب)رقم الهاتف")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        public int BID { get; set; }
        public int Status { get; set; }

    }
}