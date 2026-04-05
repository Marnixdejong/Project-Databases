using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Building
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
