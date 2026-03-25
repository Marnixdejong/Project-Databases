using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Controllers
{
    public class DrinkOrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DrinkOrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new DrinkOrderViewModel
            {
                Students = await _context.Students.Include(s => s.Person).ToListAsync(),
                Drinks = await _context.Drinks.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order(int studentId, int drinkId, int quantity)
        {
            if (studentId == 0 || drinkId == 0 || quantity <= 0)
            {
                TempData["ErrorMessage"] = "Invalid order details.";
                return RedirectToAction(nameof(Index));
            }

            var student = await _context.Students.Include(s => s.Person).FirstOrDefaultAsync(s => s.Id == studentId);
            var drink = await _context.Drinks.FindAsync(drinkId);

            if (student == null || drink == null)
            {
                TempData["ErrorMessage"] = "Student or Drink not found.";
                return RedirectToAction(nameof(Index));
            }

            if (drink.Stock < quantity)
            {
                TempData["ErrorMessage"] = $"Not enough stock for {drink.Name}. Available: {drink.Stock}.";
                return RedirectToAction(nameof(Index));
            }

            // Create order and update stock
            var order = new DrinkOrder
            {
                StudentId = studentId,
                DrinkId = drinkId,
                Quantity = quantity,
                OrderDate = DateTime.Now
            };

            drink.Stock -= quantity;

            _context.DrinkOrders.Add(order);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = $"Order processed! {quantity}x {drink.Name} sold to {student.Person?.FirstName} {student.Person?.LastName}.";

            return RedirectToAction(nameof(Index));
        }
    }
}