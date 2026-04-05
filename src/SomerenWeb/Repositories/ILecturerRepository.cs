using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public interface ILecturerRepository
    {
        List<Lecturer> GetAll(string? searchLastName = null);
        Lecturer? GetById(int id);
        void Create(Lecturer lecturer);
        void Update(Lecturer lecturer);
        void Delete(int id);
        bool LecturerNameExists(string firstName, string lastName);
    }
}
