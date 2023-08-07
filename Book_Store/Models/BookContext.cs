using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Book_Store.Models
{
    public class BookContext : DbContext
    {
        public BookContext() : base("Book")
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> publishers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ShippingCost> shippingCosts { get; set; }
    }
}