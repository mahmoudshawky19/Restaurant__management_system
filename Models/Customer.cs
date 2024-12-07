using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Models
{
    public class Customer
    {
        public int Id { set; get;  }
        public string Name { set; get; }    
        public string phoneNumber { set; get; }
        public string Address {  set; get; }
        public string Email {  set; get; }
        public string ZipCode {  set; get; }
        public ICollection<Reservation> reservations { set; get; }= new List<Reservation>();
        public ICollection<Order> orders { set; get; } = new List<Order>();

    }
}
