using Application.Pagination;
using Application.QueryParameters;
using Contracts.Dto;

namespace Application.Interfaces
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
