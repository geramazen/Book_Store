using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class Author
    {
        [Key]
        public int AID { get; set; }
        [Display(Name = "الإسم الأول")]
        public string FName { get; set; }
        [Display(Name = "الإسم الأخير")]
        public string LName { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Book> Books { get; set; }
    }
}