using Microsoft.AspNetCore.Mvc;
using SomerenWeb.Models;
using SomerenWeb.Repositories;

namespace SomerenWeb.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly IActivityRepository _repository;

        public ActivitiesController(IActivityRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(string? searchName)
        {
            try
            {
                var activities = _repository.GetAll(searchName);
                ViewData["SearchName"] = searchName;
                return View(activities);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load activities.";
                return View(new List<Activity>());
            }
        }

        public IActionResult Create()
        {
            return View(new Activity { StartTime = DateTime.Today, EndTime = DateTime.Today });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Activity activity)
        {
            if (!ModelState.IsValid) return View(activity);
            try
            {
                if (_repository.ActivityNameExists(activity.Name))
                {
                    ModelState.AddModelError("Name", "An activity with this name already exists.");
                    return View(activity);
                }
                _repository.Create(activity);
                TempData["SuccessMessage"] = "Activity added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not save the activity.";
                return View(activity);
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var activity = _repository.GetById(id);
                if (activity == null) return NotFound();
                return View(activity);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the activity.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Activity activity)
        {
            if (!ModelState.IsValid) return View(activity);
            try
            {
                _repository.Update(activity);
                TempData["SuccessMessage"] = "Activity updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not update the activity.";
                return View(activity);
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var activity = _repository.GetById(id);
                if (activity == null) return NotFound();
                return View(activity);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load the activity.";
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
                TempData["SuccessMessage"] = "Activity deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not delete the activity.";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult ManageSupervisors(int id)
        {
            try
            {
                var activity = _repository.GetById(id);
                if (activity == null) return NotFound();
                var viewModel = new ActivitySupervisorsViewModel
                {
                    Activity = activity,
                    Supervisors = _repository.GetSupervisors(id),
                    NonSupervisors = _repository.GetNonSupervisors(id)
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load supervisor data.";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AddSupervisor(int activityId, int lecturerId)
        {
            try
            {
                var activity = _repository.GetById(activityId);
                _repository.AddSupervisor(activityId, lecturerId);
                TempData["SuccessMessage"] = $"Supervisor added to {activity?.Name}.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not add the supervisor.";
            }
            return RedirectToAction(nameof(ManageSupervisors), new { id = activityId });
        }

        public IActionResult RemoveSupervisor(int activityId, int lecturerId)
        {
            try
            {
                var activity = _repository.GetById(activityId);
                _repository.RemoveSupervisor(activityId, lecturerId);
                TempData["SuccessMessage"] = $"Supervisor removed from {activity?.Name}.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not remove the supervisor.";
            }
            return RedirectToAction(nameof(ManageSupervisors), new { id = activityId });
        }

        public IActionResult ManageParticipants(int id)
        {
            try
            {
                var activity = _repository.GetById(id);
                if (activity == null) return NotFound();
                var viewModel = new ActivityParticipantsViewModel
                {
                    Activity = activity,
                    Participants = _repository.GetParticipants(id),
                    NonParticipants = _repository.GetNonParticipants(id)
                };
                return View(viewModel);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load participant data.";
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult AddParticipant(int activityId, int studentId)
        {
            try
            {
                var activity = _repository.GetById(activityId);
                _repository.AddParticipant(activityId, studentId);
                TempData["SuccessMessage"] = $"Participant added to {activity?.Name}.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not add the participant.";
            }
            return RedirectToAction(nameof(ManageParticipants), new { id = activityId });
        }

        public IActionResult RemoveParticipant(int activityId, int studentId)
        {
            try
            {
                var activity = _repository.GetById(activityId);
                _repository.RemoveParticipant(activityId, studentId);
                TempData["SuccessMessage"] = $"Participant removed from {activity?.Name}.";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not remove the participant.";
            }
            return RedirectToAction(nameof(ManageParticipants), new { id = activityId });
        }
    }
}
