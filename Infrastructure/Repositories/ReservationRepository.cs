using Application.Interfaces;
using Application.QueryParameters;
using Contracts.Dto;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    internal class ReservationRepository : IReservationRepository
    {
        private readonly DataContext dataContext;

        private static readonly Dictionary<string,
            Expression<Func<Reservation, object>>> columnSelector =
            new()
            {
                    {nameof(Reservation.Id), x => x.Id},
                    {nameof(Reservation.User.Email), x => x.User.Email},
                    {nameof(Reservation.StartDate), x => x.StartDate}
            };

        public ReservationRepository(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task<IQueryable<Reservation>> GetAllReservationsAsync(ReservationQueryParameters queryParameters)
        {
            var reservations = dataContext.Reservations
                   .Include(r => r.User).Where(r =>
                               (string.IsNullOrEmpty(queryParameters.Search)
                                            || r.UserId.ToString() == queryParameters.Search
                                            || r.User.Email.Contains(queryParameters.Search))
                                      && (queryParameters.StartDate == new DateTime(0001, 01, 01)
                                            || r.StartDate >= queryParameters.StartDate));

            if (string.IsNullOrEmpty(queryParameters.SortBy))
            {
                reservations.OrderBy(u => u.Id);
            }
            else
            {
                var selectedColumn = columnSelector[queryParameters.SortBy];
                reservations = queryParameters.SortDirection == SortDirection.ASC
                    ? reservations.OrderBy(selectedColumn)
                    : reservations.OrderByDescending(selectedColumn);
            }

            if (!await reservations.AnyAsync())
            {
                throw new NotFoundException("No reservations matching search request");
            }

            return reservations;
        }

        public async Task<IQueryable<Reservation>> GetReservationByIdAsync(int id)
        {
            var reservation = dataContext
                .Reservations
                .Include(r => r.User)
                .Where(r => r.Id == id);

            if (!await reservation.AnyAsync())
            {
                throw new NotFoundException($"Resrvation with id {id} not found");
            }

            return reservation;
        }

        public async Task AddReservationToDataBase(Reservation reservation)
        {
            await dataContext.Reservations.AddAsync(reservation);
            await dataContext.SaveChangesAsync();
        }

        public async Task EditReservationDetailsAsync(int id, EditReservationDto dto)
        {
            var reservation = await dataContext
                .Reservations
                .FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException($"Resrvation with id {id} not found");

            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;

            await dataContext.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await dataContext
                .Reservations
                .FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException($"Resrvation with id {id} not found");

            dataContext.Reservations.Remove(reservation);
            await dataContext.SaveChangesAsync();
        }
    }
}
