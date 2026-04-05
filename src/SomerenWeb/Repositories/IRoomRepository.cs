using SomerenWeb.Models;

namespace SomerenWeb.Repositories
{
    public interface IRoomRepository
    {
        List<Room> GetAll(int? minBeds = null);
        Room? GetById(int id);
        List<Building> GetAllBuildings();
        void Create(Room room);
        void Update(Room room);
        void Delete(int id);
        bool RoomNumberExists(string roomNumber);
    }
}
