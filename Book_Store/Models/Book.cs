﻿using System;
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
        [Display(Name = "دار النشر")]
        public int PID { get; set; } 
        [Display(Name = "السعر")]
        public decimal Price { get; set; }
        [Display(Name = "عدد النسخ")]
        public int AvailableCopies { get; set; }
        [Display(Name = "عدد المجلدات")]
        public int VolumesNum { get; set; }
        public decimal? Rate { get; set; }
        public int? NumberOfRates { get; set; }
        [Display(Name = "تاريخ الإضافة")]
        public DateTime EntryDate { get; set; }
        public int WatchersCount { get; set; }
        [Display(Name = "حالة الكتاب")]
        public int BookStaus { get; set; }
        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
        public virtual Publisher Publisher { get; set; }
    }
}