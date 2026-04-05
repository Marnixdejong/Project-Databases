using Microsoft.Data.SqlClient;
using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public class LecturerRepository : ILecturerRepository
    {
        private readonly string _connectionString;

        public LecturerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Lecturer> GetAll(string? searchLastName = null)
        {
            var lecturers = new List<Lecturer>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = BuildGetAllCommand(connection, searchLastName);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                lecturers.Add(ReadLecturer(reader));
            return lecturers;
        }

        public Lecturer? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT l.person_id, l.age, p.first_name, p.last_name, p.phone_number " +
                      "FROM Lecturer l INNER JOIN Person p ON l.person_id = p.person_id WHERE l.person_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            return reader.Read() ? ReadLecturer(reader) : null;
        }

        public void Create(Lecturer lecturer)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            lecturer.Id = InsertPerson(connection, lecturer.Person!);
            InsertLecturer(connection, lecturer);
        }

        public void Update(Lecturer lecturer)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            UpdatePerson(connection, lecturer.Person!, lecturer.Id);
            UpdateLecturer(connection, lecturer);
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            ExecuteDelete(connection, "DELETE FROM ActivitySupervisor WHERE lecturer_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM Lecturer WHERE person_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM Person WHERE person_id = @id", id);
        }

        public bool LecturerNameExists(string firstName, string lastName)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT COUNT(*) FROM Lecturer l INNER JOIN Person p ON l.person_id = p.person_id " +
                      "WHERE p.first_name = @fn AND p.last_name = @ln";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@fn", firstName);
            command.Parameters.AddWithValue("@ln", lastName);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private SqlCommand BuildGetAllCommand(SqlConnection connection, string? searchLastName)
        {
            var sql = "SELECT l.person_id, l.age, p.first_name, p.last_name, p.phone_number " +
                      "FROM Lecturer l INNER JOIN Person p ON l.person_id = p.person_id";
            if (!string.IsNullOrEmpty(searchLastName))
                sql += " WHERE p.last_name LIKE @search";
            sql += " ORDER BY p.last_name";
            var command = new SqlCommand(sql, connection);
            if (!string.IsNullOrEmpty(searchLastName))
                command.Parameters.AddWithValue("@search", $"%{searchLastName}%");
            return command;
        }

        private int InsertPerson(SqlConnection connection, Person person)
        {
            var sql = "INSERT INTO Person (first_name, last_name, phone_number) VALUES (@fn, @ln, @phone); SELECT SCOPE_IDENTITY();";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@fn", person.FirstName);
            command.Parameters.AddWithValue("@ln", person.LastName);
            command.Parameters.AddWithValue("@phone", person.TelephoneNumber);
            return Convert.ToInt32(command.ExecuteScalar());
        }

        private void InsertLecturer(SqlConnection connection, Lecturer lecturer)
        {
            var sql = "INSERT INTO Lecturer (person_id, age) VALUES (@id, @age)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", lecturer.Id);
            command.Parameters.AddWithValue("@age", lecturer.Age);
            command.ExecuteNonQuery();
        }

        private void UpdatePerson(SqlConnection connection, Person person, int personId)
        {
            var sql = "UPDATE Person SET first_name = @fn, last_name = @ln, phone_number = @phone WHERE person_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@fn", person.FirstName);
            command.Parameters.AddWithValue("@ln", person.LastName);
            command.Parameters.AddWithValue("@phone", person.TelephoneNumber);
            command.Parameters.AddWithValue("@id", personId);
            command.ExecuteNonQuery();
        }

        private void UpdateLecturer(SqlConnection connection, Lecturer lecturer)
        {
            var sql = "UPDATE Lecturer SET age = @age WHERE person_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@age", lecturer.Age);
            command.Parameters.AddWithValue("@id", lecturer.Id);
            command.ExecuteNonQuery();
        }

        private void ExecuteDelete(SqlConnection connection, string sql, int id)
        {
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        private Lecturer ReadLecturer(SqlDataReader reader)
        {
            return new Lecturer
            {
                Id = reader.GetInt32(0),
                Age = reader.GetInt32(1),
                Person = new Person
                {
                    Id = reader.GetInt32(0),
                    FirstName = reader.GetString(2),
                    LastName = reader.GetString(3),
                    TelephoneNumber = reader.IsDBNull(4) ? string.Empty : reader.GetString(4)
                }
            };
        }
    }
}
