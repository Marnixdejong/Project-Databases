using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public interface IStudentRepository
    {
        List<Student> GetAll(string? searchLastName = null);
        Student? GetById(int id);
        void Create(Student student);
        void Update(Student student);
        void Delete(int id);
        bool StudentNumberExists(string studentNumber);
    }
}
