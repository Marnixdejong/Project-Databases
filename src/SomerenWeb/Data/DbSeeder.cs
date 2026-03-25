using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Data
{
    public static class DbSeeder
    {
        public static void SeedIfEmpty(ApplicationDbContext context)
        {
            // Seed buildings
            if (!context.Buildings.Any())
            {
                context.Buildings.AddRange(
                    new Building { Name = "Hoofdgebouw" },
                    new Building { Name = "Bijgebouw" }
                );
                context.SaveChanges();
            }

            var mainBuilding = context.Buildings.First();
            var sideBuilding = context.Buildings.Skip(1).FirstOrDefault() ?? mainBuilding;

            // Seed some students
            if (!context.Students.Any())
            {
                var students = new[]
                {
                    new { First = "Jan", Last = "de Vries", Phone = "06-12345678", Number = "ST001", Class = "ICT-1A" },
                    new { First = "Lisa", Last = "Bakker", Phone = "06-23456789", Number = "ST002", Class = "ICT-1A" },
                    new { First = "Pieter", Last = "Jansen", Phone = "06-34567890", Number = "ST003", Class = "ICT-1B" },
                    new { First = "Sophie", Last = "Mulder", Phone = "06-45678901", Number = "ST004", Class = "ICT-1B" },
                    new { First = "Thomas", Last = "Visser", Phone = "06-56789012", Number = "ST005", Class = "ICT-1A" },
                };

                foreach (var s in students)
                {
                    var person = new Person { FirstName = s.First, LastName = s.Last, TelephoneNumber = s.Phone };
                    context.Persons.Add(person);
                    context.SaveChanges();
                    context.Students.Add(new Student { Id = person.Id, StudentNumber = s.Number, Class = s.Class, Person = person });
                    context.SaveChanges();
                }
            }

            // Seed some lecturers
            if (!context.Lecturers.Any())
            {
                var lecturers = new[]
                {
                    new { First = "Karin", Last = "de Boer", Phone = "06-11111111", Age = 42 },
                    new { First = "Henk", Last = "Peters", Phone = "06-22222222", Age = 55 },
                    new { First = "Maria", Last = "van Dijk", Phone = "06-33333333", Age = 38 },
                };

                foreach (var l in lecturers)
                {
                    var person = new Person { FirstName = l.First, LastName = l.Last, TelephoneNumber = l.Phone };
                    context.Persons.Add(person);
                    context.SaveChanges();
                    context.Lecturers.Add(new Lecturer { Id = person.Id, Age = l.Age, Person = person });
                    context.SaveChanges();
                }
            }

            // Seed some rooms
            if (!context.Rooms.Any())
            {
                context.Rooms.AddRange(
                    new Room { RoomNumber = "A101", BuildingId = mainBuilding.Id, Capacity = 4, IsTeacherRoom = false },
                    new Room { RoomNumber = "A102", BuildingId = mainBuilding.Id, Capacity = 4, IsTeacherRoom = false },
                    new Room { RoomNumber = "A201", BuildingId = mainBuilding.Id, Capacity = 2, IsTeacherRoom = true },
                    new Room { RoomNumber = "B101", BuildingId = sideBuilding.Id, Capacity = 6, IsTeacherRoom = false },
                    new Room { RoomNumber = "B102", BuildingId = sideBuilding.Id, Capacity = 2, IsTeacherRoom = true }
                );
                context.SaveChanges();
            }

            // Seed some drinks
            if (!context.Drinks.Any())
            {
                context.Drinks.AddRange(
                    new Drink { Name = "Coca Cola", Price = 2.50m, Stock = 100, IsAlcoholic = false },
                    new Drink { Name = "Water", Price = 1.00m, Stock = 200, IsAlcoholic = false },
                    new Drink { Name = "Beer (Heineken)", Price = 3.50m, Stock = 50, IsAlcoholic = true },
                    new Drink { Name = "Wine (Red)", Price = 4.00m, Stock = 20, IsAlcoholic = true }
                );
                context.SaveChanges();
            }
        }
    }
}
