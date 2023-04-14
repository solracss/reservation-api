using ReservationAPI.Domain.QueryParameters;

namespace ReservationAPI.Models.QueryParameters
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> GetItemsForPage<T>(this IQueryable<T> query, IBaseQueryParameters queryParameters)
        {
            return query
                .Skip((queryParameters.PageNumber - 1) * queryParameters.PageSize)
                .Take(queryParameters.PageSize);
        }
    }
}
