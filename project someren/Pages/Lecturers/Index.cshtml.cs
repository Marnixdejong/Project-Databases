using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;

namespace project_someren.Pages.Lecturers
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Lecturer> Lecturers { get; set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Lecturers != null)
            {
                Lecturers = await _context.Lecturers.OrderBy(l => l.LastName).ToListAsync();
            }
        }
    }
}