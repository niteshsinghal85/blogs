using GenericRepositoryPattern.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericRepositoryPattern.Repository
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
    }
}