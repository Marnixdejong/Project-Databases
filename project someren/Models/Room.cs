using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Room")]
    public class Room
    {
        [Key]
        [Column("roomId")]
        public int Id { get; set; }

        [Column("building")]
        public string Building { get; set; } = string.Empty;

        [Column("roomNumber")]
        public string RoomNumber { get; set; } = string.Empty;

        [Column("squareMeters")]
        public decimal SquareMeters { get; set; }

        [Column("isTeacherRoom")]
        public string IsTeacherRoom { get; set; } = string.Empty;
    }
}