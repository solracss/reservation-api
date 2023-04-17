using Application.QueryParameters;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAllUsersAsync(UserQueryParameters queryParameters);

        Task<IQueryable<User>> GetUserByIdAsync(int id);
    }
}
