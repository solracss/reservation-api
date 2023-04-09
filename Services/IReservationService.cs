using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IReservationService
    {
        Task<PagedResult<ReservationDto>> GetAllReservationsAsync(QueryParameters queryParameters);

        Task<ReservationDto> GetReservationAsync(int id);
    }
}
