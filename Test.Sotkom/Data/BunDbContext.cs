using Microsoft.EntityFrameworkCore;
using Test.Sotkom.ViewModels;

namespace Test.Sotkom.Data
{
    public class BunDbContext : DbContext
    {
        public BunDbContext(DbContextOptions<BunDbContext> options)
          : base(options)
        {
        }

        public DbSet<Models.Bun> Buns { get; set; }
    }
}