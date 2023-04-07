﻿namespace ReservationAPI.Domain
{
    public class QueryParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        private int _pageSize = 10;
        private const int maxPageSize = 50;
    }
}