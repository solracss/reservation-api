using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(QueryParameters queryParameters);

        Task<UserDto> GetUserAsync(int id);
    }
}
