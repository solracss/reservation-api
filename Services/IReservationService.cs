using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IReservationService
    {
        Task<PagedResult<ReservationDto>> GetAllReservationsAsync(ReservationQueryParameters queryParameters);

        Task<ReservationDto> GetReservationAsync(int id);
    }
}
