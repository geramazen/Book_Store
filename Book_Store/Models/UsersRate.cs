using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class UsersRate
    {
        [Key]
        public int ID { get; set; }
        public int UID { get; set; }
        public int BID { get; set; }
        public int Rate { get; set; }
    }
}