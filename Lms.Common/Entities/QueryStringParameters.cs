namespace Lms.Common.Entities
{
    public abstract class QueryStringParameters
    {
        public string SortOrder { get; set; } = string.Empty;

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;


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
    }
}
