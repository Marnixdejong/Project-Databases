using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Lecturer")]
    public class Lecturer
    {
        [Key]
        [Column("person_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column("age")]
        public int Age { get; set; }

        [ForeignKey("Id")]
        public Person? Person { get; set; }
    }
}