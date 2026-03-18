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
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

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

            modelBuilder.Entity<Activity>(entity => {
                entity.ToTable("Activity");
                entity.Property(e => e.Id).HasColumnName("activity_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.StartTime).HasColumnName("start_time");
                entity.Property(e => e.EndTime).HasColumnName("end_time");
            });

            modelBuilder.Entity<Drink>(entity => {
                entity.ToTable("Drink");
                entity.Property(e => e.Id).HasColumnName("drink_id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Price).HasColumnName("price");
                entity.Property(e => e.VatRate).HasColumnName("vat_rate");
                entity.Property(e => e.Stock).HasColumnName("stock");
            });

            modelBuilder.Entity<Order>(entity => {
                entity.ToTable("Order");
                entity.Property(e => e.Id).HasColumnName("order_id");
                entity.Property(e => e.StudentId).HasColumnName("student_id");
                entity.Property(e => e.OrderDate).HasColumnName("order_date");
            });

            modelBuilder.Entity<OrderItem>(entity => {
                entity.ToTable("OrderItem");
                entity.HasKey(oi => new { oi.OrderId, oi.DrinkId });
                entity.Property(oi => oi.OrderId).HasColumnName("order_id");
                entity.Property(oi => oi.DrinkId).HasColumnName("drink_id");
                entity.Property(oi => oi.Quantity).HasColumnName("quantity");
            });
        }
    }
}