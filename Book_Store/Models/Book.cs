using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "العنوان")]
        public string title { get; set; }
        [Display(Name = "الوصف")]
        public string description { get; set; }
        [Display(Name = "صورة الغلاف")]
        public string image { get; set; }
        [Display(Name = "المصنف")]
        public int AID { get; set; }
        [Display(Name = "القسم")]
        public int CID { get; set; }
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "عدد النسخ")]
        public int AvailableCopies { get; set; }
        
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
    }
}