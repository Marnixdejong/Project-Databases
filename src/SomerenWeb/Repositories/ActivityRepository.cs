using Microsoft.Data.SqlClient;
using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly string _connectionString;

        public ActivityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Activity> GetAll(string? searchName = null)
        {
            var activities = new List<Activity>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = BuildGetAllCommand(connection, searchName);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                activities.Add(ReadActivity(reader));
            return activities;
        }

        public Activity? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT activity_id, name, start_time, end_time FROM Activity WHERE activity_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            return reader.Read() ? ReadActivity(reader) : null;
        }

        public void Create(Activity activity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "INSERT INTO Activity (name, start_time, end_time) VALUES (@name, @start, @end)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", activity.Name);
            command.Parameters.AddWithValue("@start", activity.StartTime);
            command.Parameters.AddWithValue("@end", activity.EndTime);
            command.ExecuteNonQuery();
        }

        public void Update(Activity activity)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "UPDATE Activity SET name = @name, start_time = @start, end_time = @end WHERE activity_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", activity.Name);
            command.Parameters.AddWithValue("@start", activity.StartTime);
            command.Parameters.AddWithValue("@end", activity.EndTime);
            command.Parameters.AddWithValue("@id", activity.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            ExecuteDelete(connection, "DELETE FROM ActivitySupervisor WHERE activity_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM ActivityParticipant WHERE activity_id = @id", id);
            ExecuteDelete(connection, "DELETE FROM Activity WHERE activity_id = @id", id);
        }

        public bool ActivityNameExists(string name)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT COUNT(*) FROM Activity WHERE name = @name", connection);
            command.Parameters.AddWithValue("@name", name);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        public List<Lecturer> GetSupervisors(int activityId)
        {
            var lecturers = new List<Lecturer>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT l.person_id, l.age, p.first_name, p.last_name, p.phone_number " +
                      "FROM Lecturer l INNER JOIN Person p ON l.person_id = p.person_id " +
                      "INNER JOIN ActivitySupervisor s ON l.person_id = s.lecturer_id " +
                      "WHERE s.activity_id = @id ORDER BY p.last_name";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", activityId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                lecturers.Add(ReadLecturer(reader));
            return lecturers;
        }

        public List<Lecturer> GetNonSupervisors(int activityId)
        {
            var lecturers = new List<Lecturer>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT l.person_id, l.age, p.first_name, p.last_name, p.phone_number " +
                      "FROM Lecturer l INNER JOIN Person p ON l.person_id = p.person_id " +
                      "WHERE l.person_id NOT IN (SELECT lecturer_id FROM ActivitySupervisor WHERE activity_id = @id) " +
                      "ORDER BY p.last_name";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", activityId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                lecturers.Add(ReadLecturer(reader));
            return lecturers;
        }

        public void AddSupervisor(int activityId, int lecturerId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "IF NOT EXISTS (SELECT 1 FROM ActivitySupervisor WHERE activity_id = @aid AND lecturer_id = @lid) " +
                      "INSERT INTO ActivitySupervisor (activity_id, lecturer_id) VALUES (@aid, @lid)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@aid", activityId);
            command.Parameters.AddWithValue("@lid", lecturerId);
            command.ExecuteNonQuery();
        }

        public void RemoveSupervisor(int activityId, int lecturerId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "DELETE FROM ActivitySupervisor WHERE activity_id = @aid AND lecturer_id = @lid";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@aid", activityId);
            command.Parameters.AddWithValue("@lid", lecturerId);
            command.ExecuteNonQuery();
        }

        public List<Student> GetParticipants(int activityId)
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT s.person_id, s.student_number, s.class, p.first_name, p.last_name, p.phone_number " +
                      "FROM Student s INNER JOIN Person p ON s.person_id = p.person_id " +
                      "INNER JOIN ActivityParticipant ap ON s.person_id = ap.student_id " +
                      "WHERE ap.activity_id = @id ORDER BY p.last_name";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", activityId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                students.Add(ReadStudent(reader));
            return students;
        }

        public List<Student> GetNonParticipants(int activityId)
        {
            var students = new List<Student>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT s.person_id, s.student_number, s.class, p.first_name, p.last_name, p.phone_number " +
                      "FROM Student s INNER JOIN Person p ON s.person_id = p.person_id " +
                      "WHERE s.person_id NOT IN (SELECT student_id FROM ActivityParticipant WHERE activity_id = @id) " +
                      "ORDER BY p.last_name";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", activityId);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                students.Add(ReadStudent(reader));
            return students;
        }

        public void AddParticipant(int activityId, int studentId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "IF NOT EXISTS (SELECT 1 FROM ActivityParticipant WHERE activity_id = @aid AND student_id = @sid) " +
                      "INSERT INTO ActivityParticipant (activity_id, student_id) VALUES (@aid, @sid)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@aid", activityId);
            command.Parameters.AddWithValue("@sid", studentId);
            command.ExecuteNonQuery();
        }

        public void RemoveParticipant(int activityId, int studentId)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "DELETE FROM ActivityParticipant WHERE activity_id = @aid AND student_id = @sid";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@aid", activityId);
            command.Parameters.AddWithValue("@sid", studentId);
            command.ExecuteNonQuery();
        }

        private SqlCommand BuildGetAllCommand(SqlConnection connection, string? searchName)
        {
            var sql = "SELECT activity_id, name, start_time, end_time FROM Activity";
            if (!string.IsNullOrEmpty(searchName))
                sql += " WHERE name LIKE @search";
            sql += " ORDER BY start_time";
            var command = new SqlCommand(sql, connection);
            if (!string.IsNullOrEmpty(searchName))
                command.Parameters.AddWithValue("@search", $"%{searchName}%");
            return command;
        }

        private void ExecuteDelete(SqlConnection connection, string sql, int id)
        {
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        private Activity ReadActivity(SqlDataReader reader)
        {
            return new Activity
            {
                Id = reader.GetInt32(0),
                Name = reader.GetString(1),
                StartTime = reader.GetDateTime(2),
                EndTime = reader.GetDateTime(3)
            };
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
