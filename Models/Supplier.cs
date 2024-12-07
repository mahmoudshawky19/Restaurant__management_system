namespace Models
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public ICollection<Inventory> items { get; set; } = new List<Inventory>();

    }
}
