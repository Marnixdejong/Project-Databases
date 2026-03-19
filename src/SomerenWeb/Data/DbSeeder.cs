using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Data
{
    public static class DbSeeder
    {
        public static void SeedIfEmpty(ApplicationDbContext context)
        {
            if (!context.Buildings.Any())
            {
                context.Buildings.Add(new Building { Name = "Main Campus Building" });
                context.SaveChanges();
            }
        }
    }
}
