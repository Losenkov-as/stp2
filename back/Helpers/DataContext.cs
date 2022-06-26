using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Entities;

namespace WebApi.Helpers
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sql server database
            options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasAlternateKey(u => u.Username);
            modelBuilder.Entity<Role>()
                .HasAlternateKey(r => r.Name);
            modelBuilder.Entity<booking>()
                .HasAlternateKey(b => b.number);
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Users_Roles> Users_Roles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<booking> bookings { get; set; }
    }
}