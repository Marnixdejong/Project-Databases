namespace SomerenWeb.Models
{
    public class DrinkOrder
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int DrinkId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderDate { get; set; }
        public Student? Student { get; set; }
        public Drink? Drink { get; set; }
    }
}
