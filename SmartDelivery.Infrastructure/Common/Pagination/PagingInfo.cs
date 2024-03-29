﻿using System;

namespace SmartDelivery.Infrastructure.Common.Pagination
{
    public class PagingInfo
    {
        public int TotalItem { get; set; }

        public int ItemsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPage => (int)Math.Ceiling((decimal)TotalItem / ItemsPerPage);

        public string UrlParam { get; set; }
    }
}
