namespace Application.QueryParameters
{
    public interface IBaseQueryParameters
    {
        int PageNumber { get; set; }
        int PageSize { get; set; }
        string Search { get; set; }
        string SortBy { get; set; }
        SortDirection SortDirection { get; set; }
    }
}
