using Application.Interfaces;
using Application.QueryParameters;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    internal class UserRepository : IUserRepository
    {
        private readonly DataContext dataContext;

        private static readonly Dictionary<string,
            Expression<Func<User, object>>> columnSelector =
            new()
            {
                {nameof(User.Id), x => x.Id},
                {nameof(User.Email), x => x.Email},
                {nameof(User.DateOfBirth), x => x.DateOfBirth}
            };

        public UserRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IQueryable<User>> GetUserByIdAsync(int id)
        {
            var user = dataContext
                .Users
                .Include(x => x.Reservations)
                .Where(x => x.Id == id);

            if (!await user.AnyAsync())
            {
                throw new NotFoundException($"User with id {id} not found");
            }

            return user;
        }

        public async Task<IQueryable<User>> GetAllUsersAsync(UserQueryParameters queryParameters)
        {
            var users = dataContext
               .Users
               .Include(u => u.Reservations)
               .Where(u => queryParameters.Search == null
               || (u.FirstName.ToLower().Contains(queryParameters.Search.ToLower()))
               || (u.LastName.ToLower().Contains(queryParameters.Search.ToLower()))
               || (u.Email.ToLower().Contains(queryParameters.Search.ToLower())));

            if (string.IsNullOrEmpty(queryParameters.SortBy))
            {
                users.OrderBy(u => u.Id);
            }
            else
            {
                var selectedColumn = columnSelector[queryParameters.SortBy];
                users = queryParameters.SortDirection == SortDirection.ASC
                    ? users.OrderBy(selectedColumn)
                    : users.OrderByDescending(selectedColumn);
            }

            if (!await users.AnyAsync())
            {
                throw new NotFoundException("No users in database");
            }

            return users;
        }
    }
}
