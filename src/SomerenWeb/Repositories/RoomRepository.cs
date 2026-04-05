using Microsoft.Data.SqlClient;
using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly string _connectionString;

        public RoomRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Room> GetAll(int? minBeds = null)
        {
            var rooms = new List<Room>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = BuildGetAllCommand(connection, minBeds);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                rooms.Add(ReadRoom(reader));
            return rooms;
        }

        public Room? GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "SELECT r.room_id, r.building_id, r.room_number, r.capacity, r.is_teacher_room, b.name " +
                      "FROM Room r INNER JOIN Building b ON r.building_id = b.building_id WHERE r.room_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", id);
            using var reader = command.ExecuteReader();
            return reader.Read() ? ReadRoom(reader) : null;
        }

        public List<Building> GetAllBuildings()
        {
            var buildings = new List<Building>();
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT building_id, name FROM Building ORDER BY name", connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
                buildings.Add(new Building { Id = reader.GetInt32(0), Name = reader.GetString(1) });
            return buildings;
        }

        public void Create(Room room)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "INSERT INTO Room (building_id, room_number, capacity, is_teacher_room) VALUES (@bid, @rn, @cap, @itr)";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@bid", room.BuildingId);
            command.Parameters.AddWithValue("@rn", room.RoomNumber);
            command.Parameters.AddWithValue("@cap", room.Capacity);
            command.Parameters.AddWithValue("@itr", room.IsTeacherRoom);
            command.ExecuteNonQuery();
        }

        public void Update(Room room)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var sql = "UPDATE Room SET building_id = @bid, room_number = @rn, capacity = @cap, is_teacher_room = @itr WHERE room_id = @id";
            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@bid", room.BuildingId);
            command.Parameters.AddWithValue("@rn", room.RoomNumber);
            command.Parameters.AddWithValue("@cap", room.Capacity);
            command.Parameters.AddWithValue("@itr", room.IsTeacherRoom);
            command.Parameters.AddWithValue("@id", room.Id);
            command.ExecuteNonQuery();
        }

        public void Delete(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("DELETE FROM Room WHERE room_id = @id", connection);
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }

        public bool RoomNumberExists(string roomNumber)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            using var command = new SqlCommand("SELECT COUNT(*) FROM Room WHERE room_number = @rn", connection);
            command.Parameters.AddWithValue("@rn", roomNumber);
            return Convert.ToInt32(command.ExecuteScalar()) > 0;
        }

        private SqlCommand BuildGetAllCommand(SqlConnection connection, int? minBeds)
        {
            var sql = "SELECT r.room_id, r.building_id, r.room_number, r.capacity, r.is_teacher_room, b.name " +
                      "FROM Room r INNER JOIN Building b ON r.building_id = b.building_id";
            if (minBeds.HasValue)
                sql += " WHERE r.capacity >= @minBeds";
            sql += " ORDER BY r.room_number";
            var command = new SqlCommand(sql, connection);
            if (minBeds.HasValue)
                command.Parameters.AddWithValue("@minBeds", minBeds.Value);
            return command;
        }

        private Room ReadRoom(SqlDataReader reader)
        {
            return new Room
            {
                Id = reader.GetInt32(0),
                BuildingId = reader.GetInt32(1),
                RoomNumber = reader.GetString(2),
                Capacity = reader.GetInt32(3),
                IsTeacherRoom = reader.GetBoolean(4),
                Building = new Building { Id = reader.GetInt32(1), Name = reader.GetString(5) }
            };
        }
    }
}
