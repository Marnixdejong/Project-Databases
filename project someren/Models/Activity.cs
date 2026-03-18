using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace project_someren.Models
{
    [Table("Activity")]
    public class Activity
    {
        [Key]
        [Column("activityId")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("startTime")]
        public DateTime StartTime { get; set; }

        [Column("endTime")]
        public DateTime EndTime { get; set; }
    }
}
