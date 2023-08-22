using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "الإسم")]
        public string Name { get; set; }
        [Display(Name = "العنوان")]
        public string Adress { get; set; }
        [Display(Name = "اسم الكتاب")]
        public string BookName { get; set; }
        [Display(Name = "دار النشر")]
        public string PublisherName { get; set; }
        [Display(Name = "رقم الهاتف")]
        [DataType(DataType.PhoneNumber)]
        public string MobileNumber { get; set; }
        [Display(Name = "السعر")]
        public decimal Amount { get; set; }
        public int Order_Status { get; set; }
        [Display(Name = "كود الطلب")]
        public int OrderID { get; set; }
        public string DiscountCoupon { get; set; }
    }
}