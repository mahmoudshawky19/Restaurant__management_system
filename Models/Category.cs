namespace Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<MenuItem>items { get; set; } = new List<MenuItem>();
     }
}
