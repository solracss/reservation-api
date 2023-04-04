namespace ReservationAPI.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
    }
}
