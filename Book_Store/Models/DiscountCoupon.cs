using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class DiscountCoupon
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "اسم الكوبون")]
        public string Name { get; set; }
        [Display(Name = "نسبة الخصم")]
        public decimal percentage { get; set; }

    }
}