using Microsoft.EntityFrameworkCore;
using SomerenWeb.Models;

namespace SomerenWeb.Data
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
        public DbSet<Drink> Drinks { get; set; }
        public DbSet<DrinkOrder> DrinkOrders { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<ActivitySupervisor> ActivitySupervisors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ActivitySupervisor>()
                .HasKey(a => new { a.ActivityId, a.LecturerId });
        }
    }
}