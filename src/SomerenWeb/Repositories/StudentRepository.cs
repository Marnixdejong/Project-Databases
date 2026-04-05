using Microsoft.Data.SqlClient;
using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly string _connectionString;

        public StudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Student> GetAll(string? searchLastName = null)
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = BuildGetAllCommand(connection, searchLastName);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                students.Add(ReadStudent(reader));
            return students;
        }

        public Student? GetById(int id)
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

        public void Create(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            student.Id = InsertPerson(connection, student.Person!);
            InsertStudent(connection, student);
        }

        public void Update(Student student)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            UpdatePerson(connection, student.Person!, student.Id);
            UpdateStudent(connection, student);
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            ExecuteDelete(connection, "DELETE FROM ActivityParticipant WHERE student_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM DrinkOrder WHERE student_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM Student WHERE person_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM Person WHERE person_id = @id", id);
        }

        public bool StudentNumberExists(string studentNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT COUNT(*) FROM Student WHERE student_number = @sn", connection);
            command.Parameters.AddWithValue("@sn", studentNumber);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private SqlCommand BuildGetAllCommand(SqlConnection connection, string? searchLastName)
        {
            var sql = "SELECT s.person_id, s.student_number, s.class, p.first_name, p.last_name, p.phone_number " +
                      "FROM Student s INNER JOIN Person p ON s.person_id = p.person_id";
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

        private void InsertStudent(SqlConnection connection, Student student)
        {
            var sql = "INSERT INTO Student (person_id, student_number, class) VALUES (@id, @sn, @class)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", student.Id);
            command.Parameters.AddWithValue("@sn", student.StudentNumber);
            command.Parameters.AddWithValue("@class", student.Class);
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

        private void UpdateStudent(SqlConnection connection, Student student)
        {
            var sql = "UPDATE Student SET student_number = @sn, class = @class WHERE person_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@sn", student.StudentNumber);
            command.Parameters.AddWithValue("@class", student.Class);
            command.Parameters.AddWithValue("@id", student.Id);
            command.ExecuteNonQuery();
        }

        private void ExecuteDelete(SqlConnection connection, string sql, int id)
        {
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
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
    }
}
