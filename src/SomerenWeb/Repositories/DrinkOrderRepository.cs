using Microsoft.Data.SqlClient;
using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public class DrinkOrderRepository : IDrinkOrderRepository
    {
        private readonly string _connectionString;

        public DrinkOrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAllStudents()
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT s.person_id, s.student_number, s.class, p.first_name, p.last_name, p.phone_number " +
                      "FROM Student s INNER JOIN Person p ON s.person_id = p.person_id ORDER BY p.last_name";
            using var command = new SqlCommand(sql, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                students.Add(ReadStudent(reader));
            return students;
        }

        public List<Drink> GetAllDrinks()
        {
            var drinks = new List<Drink>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT drink_id, name, stock, price, vat_rate FROM Drink ORDER BY name", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                drinks.Add(ReadDrink(reader));
            return drinks;
        }

        public Student? GetStudentById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT s.person_id, s.student_number, s.class, p.first_name, p.last_name, p.phone_number " +
                      "FROM Student s INNER JOIN Person p ON s.person_id = p.person_id WHERE s.person_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            return reader.Read() ? ReadStudent(reader) : null;
        }

        public Drink? GetDrinkById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT drink_id, name, stock, price, vat_rate FROM Drink WHERE drink_id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            return reader.Read() ? ReadDrink(reader) : null;
        }

        public void CreateOrder(DrinkOrder order)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "INSERT INTO DrinkOrder (student_id, drink_id, quantity, order_date) VALUES (@sid, @did, @qty, @date)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sid", order.StudentId);
            command.Parameters.AddWithValue("@did", order.DrinkId);
            command.Parameters.AddWithValue("@qty", order.Quantity);
            command.Parameters.AddWithValue("@date", order.OrderDate);
            command.ExecuteNonQuery();
        }

        public void UpdateDrinkStock(int drinkId, int newStock)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("UPDATE Drink SET stock = @stock WHERE drink_id = @id", connection);
            command.Parameters.AddWithValue("@stock", newStock);
            command.Parameters.AddWithValue("@id", drinkId);
            command.ExecuteNonQuery();
        }

        private Student ReadStudent(SqlDataReader reader)
        {
            return new Student
            {
                Id = reader.GetInt32(0),
                StudentNumber = reader.GetString(1),
                Class = reader.GetString(2),
                Person = new Person
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(3),
                    LastName = reader.GetString(4),
                    TelephoneNumber = reader.IsDBNull(5) ? string.Empty : reader.GetString(5)
                }
            };
        }

        private Drink ReadDrink(SqlDataReader reader)
        {
            return new Drink
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                Stock = reader.GetInt32(2),
                Price = reader.GetDecimal(3),
                VatRate = reader.GetDecimal(4)
            };
        }
    }
}
