using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReservationAPI.Authentication;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservationAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext dataContext;
        private readonly IPasswordHasher<User> passwordHasher;
        private readonly IJwtTokenGenerator jwtTokenGenerator;

        public AccountService(DataContext dataContext, IPasswordHasher<User> passwordHasher, IJwtTokenGenerator jwtTokenGenerator)
        {
            this.dataContext = dataContext;
            this.passwordHasher = passwordHasher;
            this.jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<string> Login(LoginDto dto)
        {
            var user = await VerifyIfUserExist(dto);
            return jwtTokenGenerator.GenerateToken(user);
        }

        private async Task<User> VerifyIfUserExist(LoginDto dto)
        {
            var user = await dataContext.Users
                 .Include(u => u.Role)
                 .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
            {
                throw new BadRequestException("Invalid username or password");
            }

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return user;
        }

        public async Task RegisterUserAsync(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                RoleId = dto.RoleId
            };

            var hashedPassword = passwordHasher.HashPassword(newUser, dto.Password);
            newUser.PasswordHash = hashedPassword;
            await dataContext.Users.AddAsync(newUser);
            await dataContext.SaveChangesAsync();
        }
    }
}
