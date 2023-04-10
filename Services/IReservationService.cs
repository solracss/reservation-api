using ReservationAPI.Domain;
using ReservationAPI.Domain.QueryParameters;
using ReservationAPI.Dtos;

namespace ReservationAPI.Services
{
    public interface IReservationService
    {
        Task<PagedResult<ReservationDto>> GetAllReservationsAsync(ReservationQueryParameters queryParameters);

        Task<ReservationDto> GetReservationAsync(int id);

        Task<int> CreateReservationAsync(CreateReservationDto dto);

        Task EditReservationDetailsAsync(int id, EditReservationDto dto);

        Task DeleteReservationAsync(int id);
    }
}
