
namespace Shared.Domain.Contracts
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; }
        public int TotalCount { get; }
        public int Page { get; }
        public int PageSize { get; }
        public int TotalPages { get; }

        public PaginatedResult(List<T> items, int totalCount, int page, int pageSize, int totalPages)
        {
            Items = items;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = totalPages;
        }
    }
}