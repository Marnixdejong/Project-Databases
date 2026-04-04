using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SomerenWeb.Data;
using SomerenWeb.Models;

namespace SomerenWeb.Controllers
{
    public class ActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var activities = await _context.Activities
                .OrderBy(a => a.StartTime)
                .ToListAsync();

            return View(activities);
        }

       

        public async Task<IActionResult> ManageSupervisors(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
                return NotFound();

            var supervisorIds = await _context.ActivitySupervisors
                .Where(a => a.ActivityId == id)
                .Select(a => a.LecturerId)
                .ToListAsync();

            var supervisors = await _context.Lecturers
                .Include(l => l.Person)
                .Where(l => supervisorIds.Contains(l.Id))
                .OrderBy(l => l.Person!.LastName)
                .ToListAsync();

            var nonSupervisors = await _context.Lecturers
                .Include(l => l.Person)
                .Where(l => !supervisorIds.Contains(l.Id))
                .OrderBy(l => l.Person!.LastName)
                .ToListAsync();

            var viewModel = new ActivitySupervisorsViewModel
            {
                Activity = activity,
                Supervisors = supervisors,
                NonSupervisors = nonSupervisors
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddSupervisor(int activityId, int lecturerId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                var lecturer = await _context.Lecturers
                    .Include(l => l.Person)
                    .FirstOrDefaultAsync(l => l.Id == lecturerId);

                if (activity == null || lecturer == null)
                {
                    TempData["ErrorMessage"] = "Activity or lecturer not found.";
                    return RedirectToAction(nameof(Index));
                }

                var exists = await _context.ActivitySupervisors
                    .AnyAsync(a => a.ActivityId == activityId && a.LecturerId == lecturerId);

                if (!exists)
                {
                    _context.ActivitySupervisors.Add(new ActivitySupervisor
                    {
                        ActivityId = activityId,
                        LecturerId = lecturerId
                    });
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{lecturer.Person?.FirstName} {lecturer.Person?.LastName} added as supervisor for {activity.Name}.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong while adding the supervisor.";
            }

            return RedirectToAction(nameof(ManageSupervisors), new { id = activityId });
        }

        public async Task<IActionResult> RemoveSupervisor(int activityId, int lecturerId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                var lecturer = await _context.Lecturers
                    .Include(l => l.Person)
                    .FirstOrDefaultAsync(l => l.Id == lecturerId);

                if (activity == null || lecturer == null)
                {
                    TempData["ErrorMessage"] = "Activity or lecturer not found.";
                    return RedirectToAction(nameof(Index));
                }

                var link = await _context.ActivitySupervisors
                    .FirstOrDefaultAsync(a => a.ActivityId == activityId && a.LecturerId == lecturerId);

                if (link != null)
                {
                    _context.ActivitySupervisors.Remove(link);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{lecturer.Person?.FirstName} {lecturer.Person?.LastName} removed as supervisor for {activity.Name}.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong while removing the supervisor.";
            }

            return RedirectToAction(nameof(ManageSupervisors), new { id = activityId });
        }


        public async Task<IActionResult> ManageParticipants(int id)
        {
            var activity = await _context.Activities.FindAsync(id);
            if (activity == null)
                return NotFound();

            var participantIds = await _context.ActivityParticipants
                .Where(a => a.ActivityId == id)
                .Select(a => a.StudentId)
                .ToListAsync();

            var participants = await _context.Students
                .Include(s => s.Person)
                .Where(s => participantIds.Contains(s.Id))
                .OrderBy(s => s.Person!.LastName)
                .ToListAsync();

            var nonParticipants = await _context.Students
                .Include(s => s.Person)
                .Where(s => !participantIds.Contains(s.Id))
                .OrderBy(s => s.Person!.LastName)
                .ToListAsync();

            var viewModel = new ActivityParticipantsViewModel
            {
                Activity = activity,
                Participants = participants,
                NonParticipants = nonParticipants
            };

            return View(viewModel);
        }

        public async Task<IActionResult> AddParticipant(int activityId, int studentId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                var student = await _context.Students
                    .Include(s => s.Person)
                    .FirstOrDefaultAsync(s => s.Id == studentId);

                if (activity == null || student == null)
                {
                    TempData["ErrorMessage"] = "Activity or student not found.";
                    return RedirectToAction(nameof(Index));
                }

                var exists = await _context.ActivityParticipants
                    .AnyAsync(a => a.ActivityId == activityId && a.StudentId == studentId);

                if (!exists)
                {
                    _context.ActivityParticipants.Add(new ActivityParticipant
                    {
                        ActivityId = activityId,
                        StudentId = studentId
                    });
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{student.Person?.FirstName} {student.Person?.LastName} added as participant for {activity.Name}.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong while adding the participant.";
            }

            return RedirectToAction(nameof(ManageParticipants), new { id = activityId });
        }

        public async Task<IActionResult> RemoveParticipant(int activityId, int studentId)
        {
            try
            {
                var activity = await _context.Activities.FindAsync(activityId);
                var student = await _context.Students
                    .Include(s => s.Person)
                    .FirstOrDefaultAsync(s => s.Id == studentId);

                if (activity == null || student == null)
                {
                    TempData["ErrorMessage"] = "Activity or student not found.";
                    return RedirectToAction(nameof(Index));
                }

                var link = await _context.ActivityParticipants
                    .FirstOrDefaultAsync(a => a.ActivityId == activityId && a.StudentId == studentId);

                if (link != null)
                {
                    _context.ActivityParticipants.Remove(link);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = $"{student.Person?.FirstName} {student.Person?.LastName} removed as participant for {activity.Name}.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Something went wrong while removing the participant.";
            }

            return RedirectToAction(nameof(ManageParticipants), new { id = activityId });
        }
    }
}
