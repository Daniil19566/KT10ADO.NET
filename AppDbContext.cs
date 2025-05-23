using Microsoft.EntityFrameworkCore;
using KT10ADO.NET.Models;
namespace KT10ADO.NET
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<UserProfile>(p => p.UserProfileId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
