using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        [Column("order_id")]
        public int Id { get; set; }

        [Required]
        [Column("student_id")]
        public int StudentId { get; set; }

        [Required]
        [Column("order_date")]
        public DateTime OrderDate { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
