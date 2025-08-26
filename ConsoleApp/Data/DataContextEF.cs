using System.Security.Cryptography;
using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ConsoleApp.Data
{
    public class DataContextEF : DbContext
    {

        private readonly string _connectionString;
        public DbSet<Computer>? Computer { get; set; }

        public DataContextEF(IConfiguration config) { 

            // Controlliamo 'config' stesso
            _ = config ?? throw new ArgumentNullException(nameof(config));

            // Controlliamo il risultato di GetConnectionString e lanciamo un'eccezione se è nullo
            _connectionString = config.GetConnectionString("DefaultConnection")
                                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' non trovata in appsettings.json");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if(!options.IsConfigured)
            {
                options.UseSqlServer(
                    _connectionString,
                    options => options.EnableRetryOnFailure()
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("TutorialAppSchema");

            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
        }
    }    
}