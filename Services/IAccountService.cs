using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IAccountService
    {
        Task<string> GenerateToken(LoginDto dto);

        Task RegisterUserAsync(RegisterUserDto dto);
    }
}
