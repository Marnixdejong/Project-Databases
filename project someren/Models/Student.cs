using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Column("person_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [Column("student_number")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required]
        [Column("class")]
        public string Class { get; set; } = string.Empty;

        [ForeignKey("Id")]
        public Person? Person { get; set; }
    }
}
