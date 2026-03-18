using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;
using System.Linq;

namespace project_someren.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context) { _context = context; }

        public IActionResult Index() => View(_context.Students.Include(s => s.Person).ToList());

        public IActionResult Create() => View();
        [HttpPost] [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student) {
            if (ModelState.IsValid) {
                if (_context.Students.Any(s => s.StudentNumber == student.StudentNumber)) {
                    ModelState.AddModelError("StudentNumber", "This student number is already in use.");
                    return View(student);
                }
                _context.Persons.Add(student.Person!);
                await _context.SaveChangesAsync();
                student.Id = student.Person!.Id;
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public IActionResult Edit(int id) {
            var obj = _context.Students.Include(s => s.Person).FirstOrDefault(s => s.Id == id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost] [ValidateAntiForgeryToken]
        public IActionResult Edit(Student obj) {
            if (ModelState.IsValid) { _context.Students.Update(obj); _context.SaveChanges(); return RedirectToAction(nameof(Index)); }
            return View(obj);
        }

        public IActionResult Delete(int id) {
            var obj = _context.Students.Include(s => s.Person).FirstOrDefault(s => s.Id == id);
            if (obj == null) return NotFound(); return View(obj);
        }
        [HttpPost, ActionName("Delete")] [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id) {
            var obj = _context.Students.Include(s => s.Person).FirstOrDefault(s => s.Id == id);
            if (obj != null) { _context.Students.Remove(obj); _context.Persons.Remove(obj.Person); _context.SaveChanges(); }
            return RedirectToAction(nameof(Index));
        }
    }
}