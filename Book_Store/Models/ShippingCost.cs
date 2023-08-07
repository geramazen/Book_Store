using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class ShippingCost
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "سعر التوصيل")]
        public decimal Cost { get; set; }
    }
}