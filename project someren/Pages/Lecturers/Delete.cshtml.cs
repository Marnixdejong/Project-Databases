using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;

namespace project_someren.Pages.Lecturers
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
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

            var lecturer = await _context.Lecturers.FirstOrDefaultAsync(m => m.Id == id);

            if (lecturer == null)
            {
                return NotFound();
            }
            else 
            {
                Lecturer = lecturer;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Lecturers == null)
            {
                return NotFound();
            }
            var lecturer = await _context.Lecturers.FindAsync(id);

            if (lecturer != null)
            {
                Lecturer = lecturer;
                _context.Lecturers.Remove(Lecturer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}