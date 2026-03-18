using Microsoft.AspNetCore.Mvc;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class DrinksController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DrinksController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Drinks.OrderBy(d => d.Name).ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Create(Drink obj) {
            if (ModelState.IsValid) {
                if (_context.Drinks.Any(d => d.Name == obj.Name)) {
                    ModelState.AddModelError("Name", "A drink with this name already exists.");
                    return View(obj);
                }
                _context.Drinks.Add(obj); 
                _context.SaveChanges(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(obj);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Drinks.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Drink obj) {
            if (ModelState.IsValid) { 
                _context.Drinks.Update(obj); 
                _context.SaveChanges(); 
                return RedirectToAction(nameof(Index)); 
            }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Drinks.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Drinks.Find(id);
            if (obj != null) { _context.Drinks.Remove(obj); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}
