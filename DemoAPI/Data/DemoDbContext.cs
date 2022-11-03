using DemoAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoAPI.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}
