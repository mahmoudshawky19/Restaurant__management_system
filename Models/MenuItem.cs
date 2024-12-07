namespace Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public double PriceAfter { get; set; }
        public double PriceBefore { get; set; }
        public string Description { get; set; }
          public Category Category { get; set; }
        public int CategoryId {  get; set; }
        public ICollection<OrderMenuItem> orderMenuItems { set; get; } = new List<OrderMenuItem>();

    }
}
