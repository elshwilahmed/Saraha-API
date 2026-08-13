using Microsoft.EntityFrameworkCore;
using SarahaAPI.Models;

namespace SarahaAPI.Data
{
    public class SarahaDbContext : DbContext
    {
     

        public SarahaDbContext(DbContextOptions<SarahaDbContext> options) : base(options)
        {
        }

        public DbSet<Message> Messages { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.FullName)
                .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
        }


    }
}
