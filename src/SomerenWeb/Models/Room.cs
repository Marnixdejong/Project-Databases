using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [Column("room_id")]
        public int Id { get; set; }

        [Required]
        [Column("building_id")]
        public int BuildingId { get; set; }

        [Required]
        [Column("room_number")]
        public string RoomNumber { get; set; } = string.Empty;

        [Required]
        [Column("capacity")]
        public int Capacity { get; set; }

        [Required]
        [Column("is_teacher_room")]
        public bool IsTeacherRoom { get; set; }

        [ForeignKey("BuildingId")]
        public Building? Building { get; set; }
    }
}