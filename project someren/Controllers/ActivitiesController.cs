
using Microsoft.AspNetCore.Mvc;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ActivitiesController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Activities.ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Create(Activity obj) {
            if (ModelState.IsValid) { _context.Activities.Add(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Activities.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity obj) {
            if (ModelState.IsValid) { _context.Activities.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Activities.Find(id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Activities.Find(id);
            if (obj != null) { _context.Activities.Remove(obj); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}