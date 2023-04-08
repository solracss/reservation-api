using ReservationAPI.Domain;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IReservationService
    {
        Task<PagedResult<ReservationDto>> GetAllReservations(QueryParameters queryParameters);
    }
}
