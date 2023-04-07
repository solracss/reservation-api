using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
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
                throw new NotFoundException($"User with id {id} not found");
            }
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(QueryParameters queryParameters)
        {
            var users = dataContext
                .Users
                .Include(x => x.Reservations);

            if (!users.Any())
            {
                throw new NotFoundException("No users in database");
            }

            var totalItemsCount = users.Count();

            var usersResultForPage = await PagedResult<User>.GetItemsForPage(users, queryParameters);

            var userDtos = mapper.Map<List<UserDto>>(usersResultForPage);

            var pagedResult = new PagedResult<UserDto>(userDtos, totalItemsCount, queryParameters.PageSize, queryParameters.PageNumber);

            return pagedResult;
        }
    }
}
