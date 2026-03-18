using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project_someren.Data;
using project_someren.Models;

namespace project_someren.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Room Room { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.Rooms == null || Room == null)
            {
                return Page();
            }

            // Validation: Impossible to add a room that has already been added (with the same number)
            if (_context.Rooms.Any(r => r.RoomNumber == Room.RoomNumber))
            {
                ModelState.AddModelError("Room.RoomNumber", $"A room with room number {Room.RoomNumber} already exists. Could not add room.");
                return Page();
            }

            _context.Rooms.Add(Room);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}