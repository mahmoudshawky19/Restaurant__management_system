namespace Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public RestaurantTable RestaurantTable { get; set; }
        public int RestaurantTableId { get; set; }
        public DateTime ReservationDate {  get; set; }
        public int numberOfGuests {  get; set; }
        public TimeSpan Time {  get; set; }
    }
}
