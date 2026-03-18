using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("personId")]
        public int Id { get; set; }

        [Column("firstName")]
        public string FirstName { get; set; } = string.Empty;

        [Column("lastName")]
        public string LastName { get; set; } = string.Empty;

        [Column("phoneNumber")]
        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
