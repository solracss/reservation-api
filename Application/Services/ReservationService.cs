using Application.Interfaces;
using Application.Pagination;
using Application.QueryParameters;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts.Dto;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    internal class ReservationService : IReservationService
    {
        private readonly IMapper mapper;
        private readonly IReservationRepository reservationRepository;
        private readonly IHttpContextService httpContextService;

        public ReservationService(IMapper mapper, IReservationRepository reservationRepository, IHttpContextService httpContextService)
        {
            this.mapper = mapper;
            this.reservationRepository = reservationRepository;
            this.httpContextService = httpContextService;
        }

        public async Task<int> CreateReservationAsync(CreateReservationDto dto)
        {
            var userId = httpContextService.GetUserId();
            var reservation = mapper.Map<Reservation>(dto);
            reservation.UserId = userId;
            await reservationRepository.AddReservationToDataBase(reservation);

            return reservation.Id;
        }

        public async Task DeleteReservationAsync(int id)
        {
            await reservationRepository.DeleteReservationAsync(id);
        }

        public async Task EditReservationDetailsAsync(int id, EditReservationDto dto)
        {
            await reservationRepository.EditReservationDetailsAsync(id, dto);
        }

        public async Task<PagedResult<ReservationDto>> GetAllReservationsAsync(ReservationQueryParameters queryParameters)
        {
            var reservations = await reservationRepository.GetAllReservationsAsync(queryParameters);

            var totalItemsCount = reservations.Count();
            var pagedResult = await reservations.GetItemsForPage(queryParameters)
                .ProjectTo<ReservationDto>(mapper.ConfigurationProvider).ToListAsync();

            return new PagedResult<ReservationDto>(
                pagedResult,
                totalItemsCount,
                queryParameters.PageSize,
                queryParameters.PageNumber);
        }

        public async Task<ReservationDto> GetReservationAsync(int id)
        {
            var reservation = await reservationRepository.GetReservationByIdAsync(id);
            var reservationDto = reservation.ProjectTo<ReservationDto>(mapper.ConfigurationProvider);
            return await reservationDto.SingleAsync();
        }
    }
}
