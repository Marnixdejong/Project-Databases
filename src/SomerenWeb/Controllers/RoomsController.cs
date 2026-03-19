using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Controllers
{
    public class RoomsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RoomsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var rooms = await _context.Rooms.Include(r => r.Building).ToListAsync();
            return View(rooms);
        }

        public IActionResult Create()
        {
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (!ModelState.IsValid)
            {
                ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Name", room.BuildingId);
                return View(room);
            }

            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var room = await _context.Rooms.Include(r => r.Building).FirstOrDefaultAsync(r => r.Id == id);
            if (room == null)
                return NotFound();
            ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Name", room.BuildingId);
            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Room updated)
        {
            if (!ModelState.IsValid)
            {
                ViewData["BuildingId"] = new SelectList(_context.Buildings, "Id", "Name", updated.BuildingId);
                return View(updated);
            }

            var existing = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == updated.Id);
            if (existing == null)
                return NotFound();

            existing.RoomNumber = updated.RoomNumber;
            existing.BuildingId = updated.BuildingId;
            existing.Capacity = updated.Capacity;
            existing.IsTeacherRoom = updated.IsTeacherRoom;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var room = await _context.Rooms.Include(r => r.Building).FirstOrDefaultAsync(r => r.Id == id);
            if (room == null)
                return NotFound();
            return View(room);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == id);
            if (room != null)
            {
                _context.Rooms.Remove(room);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}