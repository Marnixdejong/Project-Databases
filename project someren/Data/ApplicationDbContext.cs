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
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Lecturer>().ToTable("Lecturer");
            modelBuilder.Entity<Room>().ToTable("Room");

            // Seed test data
            modelBuilder.Entity<Lecturer>().HasData(
                new Lecturer { Id = 1, FirstName = "John", LastName = "Doe", TelephoneNumber = "123456789", Age = 45 },
                new Lecturer { Id = 2, FirstName = "Jane", LastName = "Smith", TelephoneNumber = "987654321", Age = 38 },
                new Lecturer { Id = 3, FirstName = "Alan", LastName = "Turing", TelephoneNumber = "555555555", Age = 41 }
            );

            // Seed test rooms
            modelBuilder.Entity<Room>().HasData(
                new Room { Id = 1, RoomNumber = "101", NumberOfBeds = 2 },
                new Room { Id = 2, RoomNumber = "102", NumberOfBeds = 4 },
                new Room { Id = 3, RoomNumber = "103", NumberOfBeds = 6 },
                new Room { Id = 4, RoomNumber = "201", NumberOfBeds = 2 }
            );
        }
    }
}