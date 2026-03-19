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

        public DbSet<Person> Persons { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity => {
                entity.ToTable("Person");
                entity.Property(e => e.Id).HasColumnName("person_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.TelephoneNumber).HasColumnName("phone_number");
            });

            modelBuilder.Entity<Student>(entity => {
                entity.ToTable("Student");
                entity.Property(e => e.Id).HasColumnName("person_id");
                entity.Property(e => e.StudentNumber).HasColumnName("student_number");
                entity.Property(e => e.Class).HasColumnName("class");
            });

            modelBuilder.Entity<Lecturer>(entity => {
                entity.ToTable("Lecturer");
                entity.Property(e => e.Id).HasColumnName("person_id");
                entity.Property(e => e.Age).HasColumnName("age");
            });

            modelBuilder.Entity<Building>(entity => {
                entity.ToTable("Building");
                entity.Property(e => e.Id).HasColumnName("building_id");
                entity.Property(e => e.Name).HasColumnName("name");
            });

            modelBuilder.Entity<Room>(entity => {
                entity.ToTable("Room");
                entity.Property(e => e.Id).HasColumnName("room_id");
                entity.Property(e => e.BuildingId).HasColumnName("building_id");
                entity.Property(e => e.RoomNumber).HasColumnName("room_number");
                entity.Property(e => e.Capacity).HasColumnName("capacity");
                entity.Property(e => e.IsTeacherRoom).HasColumnName("is_teacher_room");
            });
        }
    }
}