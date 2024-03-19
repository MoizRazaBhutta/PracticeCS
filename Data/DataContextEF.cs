using HelloWorld.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace HelloWorld.Data
{
    // We will inherit the DbContext class to use entity framework
    // In C# we inherit using :
    public class DataContextEF : DbContext
    {
        // This is the variable Computers which has our available models
        public DbSet<Computer>? Computer{get;set;}
        private string? _connectionString;
        // Override the DbContext Method OnConfiguring
        // OnConfiguring Method is called when the DbContext Class is created or initialized
        public DataContextEF(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // If the options are not already configured
            // This will configure our database
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                options => options.EnableRetryOnFailure());
            }
            // base.OnConfiguring(optionsBuilder);
        }

        // This is for mapping model from the database to a particular table
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Change Default Schema
            modelBuilder.HasDefaultSchema("TutorialAppSchema");
            // We should also mention the particular schema
            // By default it will select dbo as schema
            // As in Azure Data Studio we have configured TutorialAppSchema as Schema and Computer as Table
            modelBuilder.Entity<Computer>()
            // If key
            .HasKey(c => c.ComputerId);
            // If no key or unique id
            // .HasNoKey();
            // .ToTable("Computer","TutorialAppSchema");
            
            
            // base.OnModelCreating(modelBuilder);
        }
    }
}