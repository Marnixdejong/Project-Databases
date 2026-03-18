using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
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

        [Required]
        [Column("price")]
        public decimal Price { get; set; }

        [Required]
        [Column("vat_rate")]
        public decimal VatRate { get; set; }

        [Required]
        [Column("stock")]
        public int Stock { get; set; }

        [NotMapped]
        public string IsLowStock => Stock < 10 ? "Low Stock" : "Sufficient";
    }
}
