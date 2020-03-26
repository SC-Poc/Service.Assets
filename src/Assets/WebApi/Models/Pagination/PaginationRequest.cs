﻿using Microsoft.AspNetCore.Mvc;

namespace Assets.WebApi.Models.Pagination
{
    public class PaginationRequest<T>
    {
        [FromQuery(Name = "order")]
        public PaginationOrder Order { get; set; }

        [FromQuery(Name = "cursor")]
        public T Cursor { get; set; }

        [FromQuery(Name = "limit")]
        public int Limit { get; set; } = 50;
    }
}
