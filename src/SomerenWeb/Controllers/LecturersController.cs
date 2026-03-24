using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LecturersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchLastName)
        {
            var query = _context.Lecturers.Include(l => l.Person).AsQueryable();

            if (!string.IsNullOrEmpty(searchLastName))
            {
                query = query.Where(l => l.Person!.LastName.Contains(searchLastName));
            }

            var lecturers = await query.OrderBy(l => l.Person!.LastName).ToListAsync();

            ViewData["SearchLastName"] = searchLastName;
            return View(lecturers);
        }

        public IActionResult Create()
        {
            return View(new Lecturer { Person = new Person() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lecturer lecturer)
        {
            if (!ModelState.IsValid)
                return View(lecturer);

            if (await _context.Persons.AnyAsync(p => p.FirstName == lecturer.Person!.FirstName && p.LastName == lecturer.Person!.LastName))
            {
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

        public async Task<IActionResult> Edit(int id)
        {
            var lecturer = await _context.Lecturers.Include(l => l.Person).FirstOrDefaultAsync(l => l.Id == id);
            if (lecturer == null)
                return NotFound();
            return View(lecturer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Lecturer updated)
        {
            if (!ModelState.IsValid)
                return View(updated);

            var existing = await _context.Lecturers.Include(l => l.Person).FirstOrDefaultAsync(l => l.Id == updated.Id);
            if (existing == null)
                return NotFound();

            existing.Age = updated.Age;
            existing.Person!.FirstName = updated.Person!.FirstName;
            existing.Person.LastName = updated.Person.LastName;
            existing.Person.TelephoneNumber = updated.Person.TelephoneNumber;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var lecturer = await _context.Lecturers.Include(l => l.Person).FirstOrDefaultAsync(l => l.Id == id);
            if (lecturer == null)
                return NotFound();
            return View(lecturer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lecturer = await _context.Lecturers.Include(l => l.Person).FirstOrDefaultAsync(l => l.Id == id);
            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
                _context.Persons.Remove(lecturer.Person!);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}