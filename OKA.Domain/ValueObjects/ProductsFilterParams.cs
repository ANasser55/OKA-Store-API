﻿
namespace OKA.Domain.ValueObjects
{
    public class ProductsFilterParams
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public string? SortColumn { get; set; }
        public string? SortBy { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
