using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class Category
    {
        [Key]
        public int CID { get; set; }
        [Display(Name = "اسم القسم")]
        public string CName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}