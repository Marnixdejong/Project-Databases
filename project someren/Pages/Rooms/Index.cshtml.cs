using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using project_someren.Data;
using project_someren.Models;

namespace project_someren.Pages.Rooms
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Room> Rooms { get; set; } = default!;

        [BindProperty(SupportsGet = true)]
        public int? FilterBeds { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<Room> query = _context.Rooms;

            if (FilterBeds.HasValue)
            {
                query = query.Where(r => r.NumberOfBeds == FilterBeds.Value);
            }

            Rooms = await query.OrderBy(r => r.RoomNumber).ToListAsync();
        }
    }
}