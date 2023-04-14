namespace ReservationAPI.Domain
{
    public class Reservation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public User User { get; set; }
    }
}
