using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;

namespace ReservationAPI.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public UserService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await dataContext
                .Users
                .Include(x => x.Reservations)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new NotFoundException($"User with id :{id} not found");
            }
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await dataContext
                .Users
                .Include(x => x.Reservations)
                .ToListAsync();

            if (!users.Any())
            {
                throw new NotFoundException("No users in database");
            }

            var userDtos = mapper.Map<List<UserDto>>(users);

            return userDtos;
        }
    }
}
