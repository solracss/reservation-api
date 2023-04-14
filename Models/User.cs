namespace ReservationAPI.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
