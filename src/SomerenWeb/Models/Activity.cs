using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Activity
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }
    }
}
