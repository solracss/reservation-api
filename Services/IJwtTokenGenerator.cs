using ReservationAPI.Domain;

namespace ReservationAPI.Services
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}
