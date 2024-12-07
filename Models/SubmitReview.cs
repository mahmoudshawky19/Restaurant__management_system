namespace Models
{
    public class SubmitReview
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Review { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public int Rating { get; set; }
    }
}
