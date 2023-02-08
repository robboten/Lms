namespace Lms.Common.Entities
{
    public class PaginationMetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
