namespace Models
{
    public enum OrderStatus
    {
        panding ,
        completed ,
        cancelled
    }
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { set; get; }
        public string totalAmount { set; get; }
        public OrderStatus Status {  set; get; }
        public double Price { get; set; }
 
        public Customer Customer { set; get; }
        public int CustomerId { set; get; }
         public ICollection<AssignedOrders> assignedOrders { set; get; } = new List<AssignedOrders>();
        public ICollection<OrderMenuItem> orderMenuItems { set; get; } = new List<OrderMenuItem>();
        public ICollection<OrderTable> orderTables { set; get; } = new List<OrderTable>();


    }
}
