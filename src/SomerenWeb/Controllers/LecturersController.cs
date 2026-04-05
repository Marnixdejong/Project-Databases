using Microsoft.AspNetCore.Mvc;
using SomerenWeb.Models;
using SomerenWeb.Repositories;

namespace SomerenWeb.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ILecturerRepository _repository;

        public LecturersController(ILecturerRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string? searchLastName)
        {
            try
            {
                var lecturers = _repository.GetAll(searchLastName);
                ViewData["SearchLastName"] = searchLastName;
                return View(lecturers);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load lecturers.";
                return View(new List<Lecturer>());
            }
        }

        public IActionResult Create()
        {
            return View(new Lecturer { Person = new Person() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Lecturer lecturer)
        {
            if (!ModelState.IsValid) return View(lecturer);
            try
            {
                if (_repository.LecturerNameExists(lecturer.Person!.FirstName, lecturer.Person.LastName))
                {
                    ModelState.AddModelError("Person.FirstName", "A lecturer with this name already exists.");
                    return View(lecturer);
                }
                _repository.Create(lecturer);
                TempData["SuccessMessage"] = "Lecturer added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not save the lecturer.";
                return View(lecturer);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var lecturer = _repository.GetById(id);
                if (lecturer == null) return NotFound();
                return View(lecturer);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the lecturer.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Lecturer lecturer)
        {
            if (!ModelState.IsValid) return View(lecturer);
            try
            {
                _repository.Update(lecturer);
                TempData["SuccessMessage"] = "Lecturer updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not update the lecturer.";
                return View(lecturer);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var lecturer = _repository.GetById(id);
                if (lecturer == null) return NotFound();
                return View(lecturer);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the lecturer.";
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
                TempData["SuccessMessage"] = "Lecturer deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not delete the lecturer. They may have related records.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
