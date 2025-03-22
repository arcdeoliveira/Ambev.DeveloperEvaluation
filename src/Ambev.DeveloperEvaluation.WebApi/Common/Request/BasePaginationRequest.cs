namespace Ambev.DeveloperEvaluation.WebApi.Common.Request
{
    public abstract class BasePaginationRequest
    {
        protected BasePaginationRequest() { }

        public int PageNumber { get; set; } = 1; 
        public int PageSize { get; set; } = 10;
        protected int PageCount { get; set; }
        protected int PageOffset { get; set; }
        protected int PageLimit { get; set; }
        protected int PageSizeLimit { get; set; }
        protected int PageCountLimit { get; set; }


    }
}
