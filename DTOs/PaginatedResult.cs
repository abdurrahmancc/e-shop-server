﻿using e_shop_server.Models;

namespace e_shop_server.DTOs
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items {get; set;} = new List<T>();
        public int TotalItems { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalItems/PageSize);
        public int StartPage { get; set; }
        public int EndPage { get; set; }

    }
}
