using Application.Pagination;
using Application.QueryParameters;
using ReservationAPI.Dto;

namespace Application.Interfaces
{
    public interface IUserService
    {
        Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters queryParameters);

        Task<UserDto> GetUserAsync(int id);
    }
}
