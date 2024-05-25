using Microsoft.EntityFrameworkCore;
using CRUD.Models;

namespace CRUD.DAL
{
    public class MyAppDbContext : DbContext
    { 
        public MyAppDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }

    }
}
