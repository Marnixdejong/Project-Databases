using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Room
    {
        public int Id { get; set; }

        public int BuildingId { get; set; }

        [Required]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        public int Capacity { get; set; }

        public bool IsTeacherRoom { get; set; }

        public Building? Building { get; set; }
    }
}
