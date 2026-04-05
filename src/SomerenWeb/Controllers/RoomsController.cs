using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SomerenWeb.Models;
using SomerenWeb.Repositories;

namespace SomerenWeb.Controllers
{
    public class RoomsController : Controller
    {
        private readonly IRoomRepository _repository;

        public RoomsController(IRoomRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int? minBeds)
        {
            try
            {
                var rooms = _repository.GetAll(minBeds);
                ViewData["MinBeds"] = minBeds;
                return View(rooms);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load rooms.";
                return View(new List<Room>());
            }
        }

        public IActionResult Create()
        {
            LoadBuildingList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Room room)
        {
            if (!ModelState.IsValid) { LoadBuildingList(room.BuildingId); return View(room); }
            try
            {
                if (_repository.RoomNumberExists(room.RoomNumber))
                {
                    ModelState.AddModelError("RoomNumber", "A room with this number already exists.");
                    LoadBuildingList(room.BuildingId);
                    return View(room);
                }
                _repository.Create(room);
                TempData["SuccessMessage"] = "Room added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not save the room.";
                LoadBuildingList(room.BuildingId);
                return View(room);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var room = _repository.GetById(id);
                if (room == null) return NotFound();
                LoadBuildingList(room.BuildingId);
                return View(room);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the room.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Room room)
        {
            if (!ModelState.IsValid) { LoadBuildingList(room.BuildingId); return View(room); }
            try
            {
                _repository.Update(room);
                TempData["SuccessMessage"] = "Room updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not update the room.";
                LoadBuildingList(room.BuildingId);
                return View(room);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var room = _repository.GetById(id);
                if (room == null) return NotFound();
                return View(room);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the room.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _repository.Delete(id);
                TempData["SuccessMessage"] = "Room deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not delete the room. It may still have assignments.";
                return RedirectToAction(nameof(Index));
            }
        }

        private void LoadBuildingList(int selectedId = 0)
        {
            var buildings = _repository.GetAllBuildings();
            ViewData["BuildingId"] = new SelectList(buildings, "Id", "Name", selectedId);
        }
    }
}
