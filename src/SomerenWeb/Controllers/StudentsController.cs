using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchLastName)
        {
            var query = _context.Students.Include(s => s.Person).AsQueryable();

            if (!string.IsNullOrEmpty(searchLastName))
            {
                query = query.Where(s => s.Person!.LastName.Contains(searchLastName));
            }

            var students = await query.OrderBy(s => s.Person!.LastName).ToListAsync();

            ViewData["SearchLastName"] = searchLastName;
            return View(students);
        }

        public IActionResult Create()
        {
            return View(new Student { Person = new Person() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid)
                return View(student);

            if (await _context.Students.AnyAsync(s => s.StudentNumber == student.StudentNumber))
            {
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

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.Include(s => s.Person).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Student updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var existing = await _context.Students.Include(s => s.Person).FirstOrDefaultAsync(s => s.Id == updated.Id);
            if (existing == null)
                return NotFound();

            existing.StudentNumber = updated.StudentNumber;
            existing.Class = updated.Class;
            existing.Person!.FirstName = updated.Person!.FirstName;
            existing.Person.LastName = updated.Person.LastName;
            existing.Person.TelephoneNumber = updated.Person.TelephoneNumber;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.Include(s => s.Person).FirstOrDefaultAsync(s => s.Id == id);
            if (student == null)
                return NotFound();
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.Include(s => s.Person).FirstOrDefaultAsync(s => s.Id == id);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.Persons.Remove(student.Person!);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}