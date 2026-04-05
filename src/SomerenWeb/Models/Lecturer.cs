using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Lecturer
    {
        public int Id { get; set; }

        [Required]
        public int Age { get; set; }

        public Person? Person { get; set; }
    }
}
