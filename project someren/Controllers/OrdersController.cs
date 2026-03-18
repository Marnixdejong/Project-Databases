using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace project_someren.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public OrdersController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index()
        {
            ViewBag.Students = new SelectList(_context.Students.Include(s => s.Person)
                .Select(s => new { Id = s.Id, Name = s.Person.FirstName + " " + s.Person.LastName }), "Id", "Name");
            ViewBag.Drinks = new SelectList(_context.Drinks.Where(d => d.Stock > 0), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder(int studentId, int drinkId, int quantity)
        {
            if (quantity <= 0) {
                TempData["Error"] = "Quantity must be at least 1.";
                return RedirectToAction(nameof(Index));
            }

            var drink = await _context.Drinks.FindAsync(drinkId);
            if (drink == null || drink.Stock < quantity) {
                TempData["Error"] = "Not enough stock or drink not found.";
                return RedirectToAction(nameof(Index));
            }

            var order = new Order {
                StudentId = studentId,
                OrderDate = DateTime.Now
            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            var orderItem = new OrderItem {
                OrderId = order.Id,
                DrinkId = drinkId,
                Quantity = quantity
            };
            _context.OrderItems.Add(orderItem);

            drink.Stock -= quantity;
            _context.Update(drink);

            await _context.SaveChangesAsync();

            TempData["Success"] = $"Order placed successfully! {quantity}x {drink.Name} ordered.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult RevenueReport()
        {
            var report = _context.OrderItems
                .Include(oi => oi.Drink)
                .Include(oi => oi.Order)
                .ToList();

            ViewBag.TotalSales = report.Sum(oi => oi.Quantity);
            ViewBag.Turnover = report.Sum(oi => oi.Quantity * (oi.Drink?.Price ?? 0));
            ViewBag.Customers = report.Select(oi => oi.Order?.StudentId).Distinct().Count();

            return View();
        }
    }
}
