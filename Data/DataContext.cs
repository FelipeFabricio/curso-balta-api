using Microsoft.EntityFrameworkCore;
using Shop_v3._1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop_v3._1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options):base(options){}

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
