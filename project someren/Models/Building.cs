using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Building")]
    public class Building
    {
        [Key]
        [Column("building_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;
    }
}
