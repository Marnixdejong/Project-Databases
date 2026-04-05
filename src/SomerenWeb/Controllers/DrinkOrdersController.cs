using Microsoft.AspNetCore.Mvc;
using SomerenWeb.Models;
using SomerenWeb.Repositories;

namespace SomerenWeb.Controllers
{
    public class DrinkOrdersController : Controller
    {
        private readonly IDrinkOrderRepository _repository;

        public DrinkOrdersController(IDrinkOrderRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index()
        {
            try
            {
                var model = new DrinkOrderViewModel
                {
                    Students = _repository.GetAllStudents(),
                    Drinks = _repository.GetAllDrinks()
                };
                return View(model);
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Could not load drink order data.";
                return View(new DrinkOrderViewModel());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Order(int studentId, int drinkId, int quantity)
        {
            if (studentId == 0 || drinkId == 0 || quantity <= 0)
            {
                TempData["ErrorMessage"] = "Invalid order details. Please select a student, a drink, and enter a valid quantity.";
                return RedirectToAction(nameof(Index));
            }
            return ProcessOrder(studentId, drinkId, quantity);
        }

        private IActionResult ProcessOrder(int studentId, int drinkId, int quantity)
        {
            try
            {
                var student = _repository.GetStudentById(studentId);
                var drink = _repository.GetDrinkById(drinkId);
                if (student == null || drink == null)
                {
                    TempData["ErrorMessage"] = "Student or drink not found.";
                    return RedirectToAction(nameof(Index));
                }
                return SaveOrder(student, drink, quantity);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Something went wrong: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private IActionResult SaveOrder(Student student, Drink drink, int quantity)
        {
            if (drink.Stock < quantity)
            {
                TempData["ErrorMessage"] = $"Not enough stock for {drink.Name}. Available: {drink.Stock}.";
                return RedirectToAction(nameof(Index));
            }
            var order = new DrinkOrder
            {
                StudentId = student.Id,
                DrinkId = drink.Id,
                Quantity = quantity,
                OrderDate = DateTime.Now
            };
            _repository.CreateOrder(order);
            _repository.UpdateDrinkStock(drink.Id, drink.Stock - quantity);
            TempData["SuccessMessage"] = $"Order processed! {quantity}x {drink.Name} for {student.Person?.FirstName} {student.Person?.LastName}.";
            return RedirectToAction(nameof(Index));
        }
    }
}
