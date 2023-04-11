﻿namespace ReservationAPI.Domain.QueryParameters
{
    public class BaseQueryParameters : IBaseQueryParameters
    {
        public string SearchString { get; set; }
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }

        public string SortBy { get; set; }
        public SortDirection SortDirection { get; set; }

        private int _pageSize = 10;
        private const int maxPageSize = 50;
    }
}
