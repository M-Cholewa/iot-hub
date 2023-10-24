using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace iot_hub_backend.Data
{
    public class IoTHubContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public IoTHubContext(DbContextOptions<IoTHubContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // USER configuration
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Email).HasColumnType("varchar(200)").IsRequired();
                user.Property(u => u.PasswordHash).HasColumnType("varchar(200)").IsRequired();
                user.Property(u => u.Roles).HasColumnType("varchar(200)");
            });

            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();
        }
    }
}
