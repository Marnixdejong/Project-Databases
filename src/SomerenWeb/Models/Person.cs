using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string TelephoneNumber { get; set; } = string.Empty;
    }
}
