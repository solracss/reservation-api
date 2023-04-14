﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;
using ReservationAPI.Models.QueryParameters;
using System.Linq.Expressions;

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

        public async Task<PagedResult<UserDto>> GetAllUsersAsync(UserQueryParameters queryParameters)
        {
            var users = dataContext
                .Users
                .Include(u => u.Reservations)
                .Where(u => queryParameters.SearchString == null
                || (u.FirstName.ToLower().Contains(queryParameters.SearchString.ToLower()))
                || (u.LastName.ToLower().Contains(queryParameters.SearchString.ToLower()))
                || (u.Email.ToLower().Contains(queryParameters.SearchString.ToLower())));

            if (string.IsNullOrEmpty(queryParameters.SortBy))
            {
                users.OrderBy(u => u.Id);
            }
            else
            {
                var columnSelector = new Dictionary<string, Expression<Func<User, object>>>
                {
                    {nameof(User.Id), x => x.Id},
                    {nameof(User.Email), x => x.Email},
                    {nameof(User.DateOfBirth), x => x.DateOfBirth}
                };

                var selectedColumn = columnSelector[queryParameters.SortBy];

                users = queryParameters.SortDirection == SortDirection.ASC
                    ? users.OrderBy(selectedColumn)
                    : users.OrderByDescending(selectedColumn);
            }

            if (!await users.AnyAsync())
            {
                throw new NotFoundException("No users in database");
            }

            var totalItemsCount = users.Count();
            var pagedResult = await users.GetItemsForPage(queryParameters).ProjectTo<UserDto>(mapper.ConfigurationProvider).ToListAsync();

            return new PagedResult<UserDto>(
                pagedResult,
                totalItemsCount,
                queryParameters.PageSize,
                queryParameters.PageNumber);
        }
    }
}
