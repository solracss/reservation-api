using Microsoft.AspNetCore.Identity;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DataContext dataContext;
        private readonly IPasswordHasher<User> passwordHasher;

        public AccountService(DataContext dataContext, IPasswordHasher<User> passwordHasher)
        {
            this.dataContext = dataContext;
            this.passwordHasher = passwordHasher;
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
