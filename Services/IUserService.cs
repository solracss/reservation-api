using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters queryParameters);

        Task<UserDto> GetUserAsync(int id);
    }
}
