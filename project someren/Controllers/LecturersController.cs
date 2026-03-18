using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LecturersController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Lecturers.Include(l => l.Person).ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecturer lecturer) {
            if (ModelState.IsValid) {
                if (_context.Persons.Any(p => p.FirstName == lecturer.Person!.FirstName && p.LastName == lecturer.Person!.LastName)) {
                    ModelState.AddModelError("Person.FirstName", "A lecturer with this name already exists.");
                    return View(lecturer);
                }
                _context.Persons.Add(lecturer.Person!);
                await _context.SaveChangesAsync();
                lecturer.Id = lecturer.Person!.Id;
                _context.Lecturers.Add(lecturer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lecturer);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Lecturers.Include(l => l.Person).FirstOrDefault(l => l.Id == id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Lecturer obj) {
            if (ModelState.IsValid) { _context.Lecturers.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Lecturers.Include(l => l.Person).FirstOrDefault(l => l.Id == id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Lecturers.Include(l => l.Person).FirstOrDefault(l => l.Id == id);
            if (obj != null) { _context.Lecturers.Remove(obj); _context.Persons.Remove(obj.Person); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}