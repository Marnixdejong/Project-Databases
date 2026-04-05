using System.ComponentModel.DataAnnotations;

namespace SomerenWeb.Models
{
    public class Drink
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int Stock { get; set; }

        public decimal Price { get; set; }

        public decimal VatRate { get; set; }
    }
}
