namespace TCMA.BLL.Models
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();

        public int Count { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }
    }
}
