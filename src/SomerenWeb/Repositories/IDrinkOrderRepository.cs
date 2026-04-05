using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public interface IDrinkOrderRepository
    {
        List<Student> GetAllStudents();
        List<Drink> GetAllDrinks();
        Student? GetStudentById(int id);
        Drink? GetDrinkById(int id);
        void CreateOrder(DrinkOrder order);
        void UpdateDrinkStock(int drinkId, int newStock);
    }
}
