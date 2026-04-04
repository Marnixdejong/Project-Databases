namespace SomerenWeb.Models
{
    public class ActivityParticipantsViewModel
    {
        public Activity Activity { get; set; } = null!;
        public List<Student> Participants { get; set; } = new List<Student>();
        public List<Student> NonParticipants { get; set; } = new List<Student>();
    }
}