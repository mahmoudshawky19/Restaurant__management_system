namespace Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ProfilePicture { get; set; }
        public string phoneNumber { get; set; }
        public string Job { get; set; }
        public double salary { get; set; }
        public ICollection<AssignedOrders> items { get; set; } = new List<AssignedOrders>();

    }
}
