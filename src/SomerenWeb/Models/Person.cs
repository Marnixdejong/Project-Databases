using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    [Table("Person")]
    public class Person
    {
        [Key]
        [Column("person_id")]
        public int Id { get; set; }

        [Required]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [Column("phone_number")]
        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
