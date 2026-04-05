using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string StudentNumber { get; set; } = string.Empty;

        [Required]
        public string Class { get; set; } = string.Empty;

        public Person? Person { get; set; }
    }
}
