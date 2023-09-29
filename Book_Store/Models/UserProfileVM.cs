using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class UserProfileVM
    {
        public User user { get; set; }
        public List<Order> userOrders { get; set; }
    }
}