
using Microsoft.AspNetCore.Mvc;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LecturersController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Lecturers.ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Create(Lecturer obj) {
            if (ModelState.IsValid) { _context.Lecturers.Add(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Lecturers.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Lecturer obj) {
            if (ModelState.IsValid) { _context.Lecturers.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Lecturers.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Lecturers.Find(id);
            if (obj != null) { _context.Lecturers.Remove(obj); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}