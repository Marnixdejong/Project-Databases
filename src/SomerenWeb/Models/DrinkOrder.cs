using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SomerenWeb.Models
{
    [Table("DrinkOrder")]
    public class DrinkOrder
    {
        [Key]
        [Column("order_id")]
        public int Id { get; set; }

        [Required]
        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        [Required]
        [Column("drink_id")]
        public int DrinkId { get; set; }

        [ForeignKey("DrinkId")]
        public Drink? Drink { get; set; }

        [Required]
        [Column("count")]
        public int Quantity { get; set; }

        [Column("order_date")]
        public DateTime OrderDate { get; set; }
    }
}