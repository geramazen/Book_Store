using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Book_Store.Models
{
    [MetadataType(typeof(BookMetaData))]
    public partial class Book  
    {

    }

    public class BookMetaData
    {
        public int ID { get; set; }
        public string title { get; set; }
        [Display(Name = "Book Description")]
        public string description { get; set; }
        public string image { get; set; }
        [Display(Name = "Author Name")]
        public int AID { get; set; }
        public int CID { get; set; }

        public virtual Author Author { get; set; }
        public virtual Category Category { get; set; }
    }
}