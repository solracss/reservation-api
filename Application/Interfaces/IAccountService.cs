using Contracts.Dto;

namespace Application.Interfaces
{
    public interface IAccountService
    {
        Task<string> LoginAsync(LoginDto dto);

        Task RegisterUserAsync(RegisterUserDto dto);
    }
}
