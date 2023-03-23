using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();

        Task<UserDto> GetUserAsync(int id);
    }
}
