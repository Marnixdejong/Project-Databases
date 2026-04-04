using System.ComponentModel.DataAnnotations.Schema;

namespace SomerenWeb.Models
{
    [Table("ActivitySupervisor")]
    public class ActivitySupervisor
    {
        [Column("activity_id")]
        public int ActivityId { get; set; }

        [ForeignKey("ActivityId")]
        public Activity? Activity { get; set; }

        [Column("lecturer_id")]
        public int LecturerId { get; set; }

        [ForeignKey("LecturerId")]
        public Lecturer? Lecturer { get; set; }
    }
}
