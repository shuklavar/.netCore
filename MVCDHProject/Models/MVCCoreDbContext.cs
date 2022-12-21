using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MVCDHProject.Models
{
    public class MVCCoreDbContext: IdentityDbContext
    {
        public MVCCoreDbContext(DbContextOptions options): base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Customer>().HasData(
               new Customer { Custid = 101, Name = "Sai", Balance = 50000.00m, City = "Delhi", Status = true },
               new Customer { Custid = 102, Name = "Pratik", Balance = 50000.00m, City = "Mumbai", Status = true },
               new Customer { Custid = 103, Name = "Mohan", Balance = 50000.00m, City = "Bangalore", Status = true },
               new Customer { Custid = 104, Name = "Mrunal", Balance = 50000.00m, City = "Kolkatta", Status = true }

                );
        }
        public DbSet<Customer> Customers { get; set; }
    }
}
