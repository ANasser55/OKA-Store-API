
namespace OKA.Application.DTOs
{
    public class PageDTO<T>
    {

        public IEnumerable<T> Items { get; }
        public int ItemsCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int LastPage => (int)Math.Ceiling(ItemsCount / (decimal)PageSize);
        public bool HasNextPage => Page < LastPage;
        public bool HasPreviousPage => Page > 1;

        public PageDTO(IEnumerable<T> items, int page, int pageSize, int itemsCount)
        {
            this.Items = items;
            this.Page = page;
            this.PageSize = pageSize;
            this.ItemsCount = itemsCount;
        }

    }
}
