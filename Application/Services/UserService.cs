using Application.Interfaces;
using Application.Pagination;
using Application.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Dto;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
        }

        public async Task<UserDto> GetUserAsync(int id)
        {
            var user = await userRepository.GetUserByIdAsync(id);
            var userDto = user.ProjectTo<UserDto>(mapper.ConfigurationProvider);

            return await userDto.SingleAsync();
        }

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters queryParameters)
        {
            var users = await userRepository.GetAllUsersAsync(queryParameters);
            var totalItemsCount = users.Count();
            var pagedResult = await users.GetItemsForPage(queryParameters).
                ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync();

            return new PagedResult<UserDto>(
                pagedResult,
                totalItemsCount,
                queryParameters.PageSize,
                queryParameters.PageNumber);
        }
    }
}
