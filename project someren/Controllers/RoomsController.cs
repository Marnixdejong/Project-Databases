
using Microsoft.AspNetCore.Mvc;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RoomsController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Rooms.ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Create(Room obj) {
            if (ModelState.IsValid) { _context.Rooms.Add(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Rooms.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Room obj) {
            if (ModelState.IsValid) { _context.Rooms.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Rooms.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Rooms.Find(id);
            if (obj != null) { _context.Rooms.Remove(obj); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}