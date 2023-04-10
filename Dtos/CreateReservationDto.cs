namespace ReservationAPI.Dtos
{
    public class CreateReservationDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
