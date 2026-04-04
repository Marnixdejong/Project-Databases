using System.ComponentModel.DataAnnotations.Schema;

namespace SomerenWeb.Models
{
    [Table("ActivityParticipant")]
    public class ActivityParticipant
    {
        [Column("activity_id")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity? Activity { get; set; }

        [Column("student_id")]
        public int StudentId { get; set; }

        [ForeignKey("StudentId")]
        public Student? Student { get; set; }
    }
}