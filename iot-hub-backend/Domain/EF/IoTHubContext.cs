using Domain.Core;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Domain.Data
{
    public class IoTHubContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Role> Roles { get; set; }

        public IoTHubContext(DbContextOptions<IoTHubContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // =========== USER ===========
            modelBuilder.Entity<User>(user =>
            {
                user.Property(u => u.Email).HasColumnType("varchar(200)").IsRequired();
                user.Property(u => u.PasswordHash).HasColumnType("varchar(200)").IsRequired();
            });

            modelBuilder.Entity<User>().HasIndex(user => user.Email).IsUnique();

            // =========== DEVICE ===========
            modelBuilder.Entity<Device>(device =>
            {
                device.Property(d => d.ApiKey).HasColumnType("varchar(200)").IsRequired();
                device.Property(d => d.DeviceTwin).HasColumnType("varchar(4096)");
            });

            modelBuilder.Entity<Device>().HasIndex(device => device.ApiKey).IsUnique();

            // =========== ROLE ===========
            modelBuilder.Entity<Role>(role =>
            {
                role.Property(r => r.Key).HasColumnType("varchar(200)").IsRequired();
            });

            modelBuilder.Entity<Role>().HasIndex(role => role.Key).IsUnique();

            // =========== USER ROLE MAPPING ===========
            modelBuilder.Entity<User>().HasMany(u => u.Roles).WithMany(r => r.Users);

            // =========== DEVICE USER MAPPING ===========
            modelBuilder.Entity<Device>().HasOne(d => d.Owner).WithMany(u => u.Devices);

        }
    }
}
