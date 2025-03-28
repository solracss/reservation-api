using Application.Interfaces;
using Application.Pagination;
using Application.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Dto;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserService(IMapper mapper, IUserRepository userRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;
            this.httpContextAccessor = httpContextAccessor;
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

        public async Task<UserDto> GetCurrentUserAsync()
        {
            var userIdClaim = (httpContextAccessor.HttpContext?.User.FindFirst("uid")) ?? throw new NotFoundException("User is not authenticated.");
            int userId = int.Parse(userIdClaim.Value);
            var user = await userRepository.GetUserByIdAsync(userId);
            var userDto = user.ProjectTo<UserDto>(mapper.ConfigurationProvider);

            return await userDto.SingleAsync();
        }
    }
}
