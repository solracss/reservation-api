using Contracts.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginDto dto);

        Task<User> RegisterUserAsync(RegisterUserDto dto);
    }
}
