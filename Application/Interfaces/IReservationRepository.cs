using Application.QueryParameters;
using Contracts.Dto;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IReservationRepository
    {
        Task<IQueryable<Reservation>> GetAllReservationsAsync(ReservationQueryParameters queryParameters);

        Task<IQueryable<Reservation>> GetReservationByIdAsync(int id);

        Task AddReservationToDataBase(Reservation reservation);

        Task EditReservationDetailsAsync(int id, EditReservationDto dto);

        Task DeleteReservationAsync(int id);
    }
}
