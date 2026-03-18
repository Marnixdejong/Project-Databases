using Microsoft.AspNetCore.Mvc;
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

        [BindProperty(SupportsGet = true)]
        public string? FilterLastName { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Lecturers != null)
            {
                var query = _context.Lecturers.AsQueryable();

                if (!string.IsNullOrEmpty(FilterLastName))
                {
                    query = query.Where(l => l.LastName.Contains(FilterLastName));
                }

                Lecturers = await query.OrderBy(l => l.LastName).ToListAsync();
            }
        }
    }
}