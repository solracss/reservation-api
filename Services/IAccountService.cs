using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IAccountService
    {
        Task RegisterUserAsync(RegisterUserDto dto);
    }
}
