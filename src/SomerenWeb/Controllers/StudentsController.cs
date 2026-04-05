using Microsoft.AspNetCore.Mvc;
using SomerenWeb.Models;
using SomerenWeb.Repositories;

namespace SomerenWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentRepository _repository;

        public StudentsController(IStudentRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string? searchLastName)
        {
            try
            {
                var students = _repository.GetAll(searchLastName);
                ViewData["SearchLastName"] = searchLastName;
                return View(students);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load students.";
                return View(new List<Student>());
            }
        }

        public IActionResult Create()
        {
            return View(new Student { Person = new Person() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            try
            {
                if (_repository.StudentNumberExists(student.StudentNumber))
                {
                    ModelState.AddModelError("StudentNumber", "This student number is already in use.");
                    return View(student);
                }
                _repository.Create(student);
                TempData["SuccessMessage"] = "Student added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not save the student.";
                return View(student);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var student = _repository.GetById(id);
                if (student == null) return NotFound();
                return View(student);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the student.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            try
            {
                _repository.Update(student);
                TempData["SuccessMessage"] = "Student updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not update the student.";
                return View(student);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var student = _repository.GetById(id);
                if (student == null) return NotFound();
                return View(student);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the student.";
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
                TempData["SuccessMessage"] = "Student deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not delete the student. They may have related records.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
