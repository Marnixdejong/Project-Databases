using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;

namespace project_someren.Pages.Lecturers
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Lecturer Lecturer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Lecturers == null)
            {
                return NotFound();
            }

            var lecturer =  await _context.Lecturers.FirstOrDefaultAsync(m => m.Id == id);
            if (lecturer == null)
            {
                return NotFound();
            }
            Lecturer = lecturer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if lecturer with the same First and Last Name already exists (excluding the current one)
            bool exists = await _context.Lecturers.AnyAsync(l => l.FirstName == Lecturer.FirstName && l.LastName == Lecturer.LastName && l.Id != Lecturer.Id);
            if (exists)
            {
                ModelState.AddModelError(string.Empty, "Another lecturer with this name already exists.");
                return Page();
            }

            _context.Attach(Lecturer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LecturerExists(Lecturer.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LecturerExists(int id)
        {
          return (_context.Lecturers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}