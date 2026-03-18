using Microsoft.EntityFrameworkCore;
using project_someren.Models;

namespace project_someren.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Lecturer> Lecturers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Seed test data
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer { Id = 1, FirstName = "John", LastName = "Doe", TelephoneNumber = "123456789", Age = 45 },
                new Lecturer { Id = 2, FirstName = "Jane", LastName = "Smith", TelephoneNumber = "987654321", Age = 38 },
                new Lecturer { Id = 3, FirstName = "Alan", LastName = "Turing", TelephoneNumber = "555555555", Age = 41 }
            );
        }
    }
}