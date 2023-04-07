using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync(QueryParameters queryParameters);

        Task<UserDto> GetUserAsync(int id);
    }
}
