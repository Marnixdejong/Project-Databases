namespace SomerenWeb.Models
{
    public class ActivitySupervisorsViewModel
    {
        public Activity Activity { get; set; } = null!;
        public List<Lecturer> Supervisors { get; set; } = new List<Lecturer>();
        public List<Lecturer> NonSupervisors { get; set; } = new List<Lecturer>();
    }
}
