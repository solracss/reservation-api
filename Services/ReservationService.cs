using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;

namespace ReservationAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;

        public ReservationService(DataContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<PagedResult<ReservationDto>> GetAllReservationsAsync(QueryParameters queryParameters)
        {
            var reservations = from r in dataContext.Reservations
                   .Include(r => r.User)
                               where (string.IsNullOrEmpty(queryParameters.SearchString)
                                            || r.UserId.ToString() == queryParameters.SearchString
                                            || r.User.Email.Contains(queryParameters.SearchString))
                                      && (queryParameters.StartDateParam == new DateTime(0001, 01, 01)
                                            || r.StartDate >= queryParameters.StartDateParam)
                               select r;

            if (!reservations.Any())
            {
                throw new NotFoundException("No reservations matching search request");
            }

            var totalItemsCount = reservations.Count();
            var reservationsResultForPage = await PagedResult<Reservation>
                .GetItemsForPage(reservations, queryParameters);
            var reservationDto = mapper.Map<List<ReservationDto>>(reservationsResultForPage);

            return new PagedResult<ReservationDto>(
                reservationDto,
                totalItemsCount,
                queryParameters.PageSize,
                queryParameters.PageNumber);
        }

        public async Task<ReservationDto> GetReservationAsync(int id)
        {
            var reservation = await dataContext
                .Reservations
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new NotFoundException($"Resrvation with id {id} not found");

            var reservationDto = mapper.Map<ReservationDto>(reservation);
            return reservationDto;
        }
    }
}
