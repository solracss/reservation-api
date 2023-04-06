namespace ReservationAPI.Authentication
{
    public class AuthenticationSettings
    {
        public const string SectionName = "Authentication";
        public string JwtKey { get; set; }
        public int JwtExpireDays { get; set; }
        public string JwtIssuer { get; set; }
    }
}
