namespace project_someren.Models
{
    public class Lecturer
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string TelephoneNumber { get; set; } = string.Empty;
        public int Age { get; set; }
    }
}