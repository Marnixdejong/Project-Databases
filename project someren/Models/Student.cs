using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        [Column("studentId")]
        public int Id { get; set; }

        [Column("studentNumber")]
        public string StudentNumber { get; set; } = string.Empty;

        [Column("classId")]
        public int ClassId { get; set; }

        [Column("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("phoneNumber")]
        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
