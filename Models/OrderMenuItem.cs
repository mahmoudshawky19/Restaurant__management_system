namespace Models
{
    public class OrderMenuItem
    {
        public int OrderMenuItemId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int MenuItemId {  get; set; }
    }
}
