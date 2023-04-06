using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IAccountService
    {
        Task<string> Login(LoginDto dto);

        Task RegisterUserAsync(RegisterUserDto dto);
    }
}
