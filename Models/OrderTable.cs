namespace Models
{
    public class OrderTable
    {
        public int OrderTableId { get; set; }
        public Order Order {  get; set; }
         public int OrderId { get; set; }
        public RestaurantTable RestaurantTable { get; set; }
        public int RestaurantTableId {  get; set; }
    }
}
