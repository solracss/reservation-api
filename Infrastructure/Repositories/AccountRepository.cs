using Application.Interfaces;
using Contracts.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class AccountRepository : IAccountRepository
    {
        private readonly DataContext dataContext;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountRepository(DataContext dataContext, IPasswordHasher<User> passwordHasher)
        {
            this.dataContext = dataContext;
            this.passwordHasher = passwordHasher;
        }

        public async Task<bool> EmailAlreadyTaken(string email)
        {
            var taken = await dataContext.Users.AnyAsync(u => u.Email == email);
            return taken;
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

        public async Task<User> VerifyIfUserExistAsync(LoginDto dto)
        {
            var user = await dataContext.Users
                 .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == dto.Email)
                 ?? throw new BadRequestException("Invalid username or password");

            var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new BadRequestException("Invalid username or password");
            }

            return user;
        }
    }
}
