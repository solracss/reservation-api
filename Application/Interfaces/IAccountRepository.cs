using Contracts.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<User> VerifyIfUserExistAsync(LoginDto dto);

        Task<User> RegisterUserAsync(RegisterUserDto dto);

        Task<bool> EmailAlreadyTaken(string email);
    }
}
