using BlogAPI.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.DataAccess
{
    public class BlogProjectDbContext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=MSI; database= BlogProjectDb; integrated security=true");
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
