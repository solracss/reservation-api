using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;
using System.Linq;

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
                .FirstOrDefaultAsync(x => x.Id == id)
                ?? throw new NotFoundException($"User with id {id} not found");
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(QueryParameters queryParameters)
        {
            var users = dataContext
                .Users
                .Include(u => u.Reservations)
                .Where(u => queryParameters.SearchString == null
                || (u.FirstName.ToLower().Contains(queryParameters.SearchString.ToLower()))
                || (u.LastName.ToLower().Contains(queryParameters.SearchString.ToLower()))
                || (u.Email.ToLower().Contains(queryParameters.SearchString.ToLower()))
                );

            if (!users.Any())
            {
                throw new NotFoundException("No users in database");
            }

            var totalItemsCount = users.Count();
            var usersResultForPage = await PagedResult<User>.GetItemsForPage(users, queryParameters);
            var userDtos = mapper.Map<List<UserDto>>(usersResultForPage);

            return new PagedResult<UserDto>(userDtos, totalItemsCount, queryParameters.PageSize, queryParameters.PageNumber);
        }
    }
}
