using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Lecturer")]
    public class Lecturer
    {
        [Key]
        [Column("lectureId")]
        public int Id { get; set; }

        [Column("age")]
        public int Age { get; set; }

        [Column("personId")]
        public int PersonId { get; set; }

        public Person? Person { get; set; }
    }
}