using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("OrderItem")]
    public class OrderItem
    {
        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("drink_id")]
        public int DrinkId { get; set; }

        [Required]
        [Column("quantity")]
        public int Quantity { get; set; }

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [ForeignKey("DrinkId")]
        public Drink? Drink { get; set; }
    }
}
