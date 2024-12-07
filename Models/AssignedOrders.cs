namespace Models
{
    public class AssignedOrders
    {
        public int Id { get; set; }
        public DateTime AssignedDate {  get; set; }
        public Employee Employee { get; set; }
        public int EmployeeId { get; set; }
        public Order Order { get; set; }
        public int OrderId { get; set; }
    }
}
