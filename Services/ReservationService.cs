using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReservationAPI.Data;
using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
using ReservationAPI.Dtos;
using ReservationAPI.Exceptions;

namespace ReservationAPI.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DataContext dataContext;
        private readonly IMapper mapper;
        private readonly IHttpContextService httpContextService;

        public ReservationService(DataContext dataContext, IMapper mapper, IHttpContextService httpContextService)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
            this.httpContextService = httpContextService;
        }

        public async Task<PagedResult<ReservationDto>> GetAllReservationsAsync(ReservationQueryParameters queryParameters)
        {
            var reservations = dataContext.Reservations
                   .Include(r => r.User).Where(r =>
                               (string.IsNullOrEmpty(queryParameters.SearchString)
                                            || r.UserId.ToString() == queryParameters.SearchString
                                            || r.User.Email.Contains(queryParameters.SearchString))
                                      && (queryParameters.StartDateParam == new DateTime(0001, 01, 01)
                                            || r.StartDate >= queryParameters.StartDateParam))
                   .ProjectTo<ReservationDto>(mapper.ConfigurationProvider);

            if (!reservations.Any())
            {
                throw new NotFoundException("No reservations matching search request");
            }

            var totalItemsCount = reservations.Count();
            var pagedResult = await PagedResult<ReservationDto>
                .GetItemsForPage(reservations, queryParameters);

            return new PagedResult<ReservationDto>(
                pagedResult,
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

        public async Task<int> CreateReservationAsync(CreateReservationDto dto)
        {
            var userId = httpContextService.GetUserId();
            var reservation = mapper.Map<Reservation>(dto);
            reservation.UserId = userId;
            await dataContext.Reservations.AddAsync(reservation);
            await dataContext.SaveChangesAsync();

            return reservation.Id;
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
            dataContext.SaveChanges();
        }
    }
}
