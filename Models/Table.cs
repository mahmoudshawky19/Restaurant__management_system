namespace  Models
{
    public class RestaurantTable
    {
        public int Id { get; set; }
        public int tableNumber { get; set; }
        public int Capacity  { get; set; }
        public string Location {  get; set; }
        public ICollection<Reservation> reservations { set; get; } = new List<Reservation>();
        public ICollection<OrderTable> orderTables { set; get; } = new List<OrderTable>();

    }
}
