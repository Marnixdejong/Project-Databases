using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public interface IActivityRepository
    {
        List<Activity> GetAll(string? searchName = null);
        Activity? GetById(int id);
        void Create(Activity activity);
        void Update(Activity activity);
        void Delete(int id);
        bool ActivityNameExists(string name);
        List<Lecturer> GetSupervisors(int activityId);
        List<Lecturer> GetNonSupervisors(int activityId);
        void AddSupervisor(int activityId, int lecturerId);
        void RemoveSupervisor(int activityId, int lecturerId);
        List<Student> GetParticipants(int activityId);
        List<Student> GetNonParticipants(int activityId);
        void AddParticipant(int activityId, int studentId);
        void RemoveParticipant(int activityId, int studentId);
    }
}
