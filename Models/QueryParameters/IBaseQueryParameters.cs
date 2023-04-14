namespace ReservationAPI.Domain.QueryParameters
{
    public interface IBaseQueryParameters
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string SearchString { get; set; }
        string SortBy { get; set; }
        SortDirection SortDirection { get; set; }
    }
}
