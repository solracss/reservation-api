using Application.Interfaces;
using Contracts.Dto;

namespace Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository accountRepository;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AccountService(IAccountRepository accountRepository, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.accountRepository = accountRepository;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await accountRepository.VerifyIfUserExistAsync(dto);
            return jwtTokenGenerator.GenerateToken(user);
        }

        public async Task RegisterUserAsync(RegisterUserDto dto)
        {
            await accountRepository.RegisterUserAsync(dto);
        }
    }
}
