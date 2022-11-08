using Microsoft.EntityFrameworkCore;
using TestAPI.Models;

namespace TestAPI.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt):base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().Property(a => a.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Employee>().Property(a => a.Id).HasDefaultValueSql("NEWID()");
            modelBuilder.Entity<Employee>().Property(a => a.DateOfJoining).HasDefaultValueSql("GETDATE()");
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }
    }
}
