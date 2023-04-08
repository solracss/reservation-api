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

        public async Task<PagedResult<ReservationDto>> GetAllReservations(QueryParameters queryParameters)
        {
            var reservations = dataContext
                .Reservations
                .Include(r => r.User)
                .Where(r => queryParameters.SearchString == null
                || (r.UserId.ToString().Equals(queryParameters.SearchString)));

            ;

            if (!reservations.Any())
            {
                throw new NotFoundException("No reservations matching search request");
            }

            var totalItemsCount = reservations.Count();
            var reservationsResultForPage = await PagedResult<Reservation>.GetItemsForPage(reservations, queryParameters);
            var reservationDto = mapper.Map<List<ReservationDto>>(reservationsResultForPage);

            return new PagedResult<ReservationDto>(reservationDto, totalItemsCount, queryParameters.PageSize, queryParameters.PageNumber);
        }
    }
}
