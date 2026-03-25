using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomerenWeb.Models
{
    [Table("Drink")]
    public class Drink
    {
        [Key]
        [Column("drink_id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("stock")]
        public int Stock { get; set; }

        [Column("price", TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column("is_alcoholic")]
        public bool IsAlcoholic { get; set; }
    }
}