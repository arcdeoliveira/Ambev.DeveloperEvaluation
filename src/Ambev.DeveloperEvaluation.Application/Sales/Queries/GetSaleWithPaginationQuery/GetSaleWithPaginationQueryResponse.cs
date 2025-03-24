using Ambev.DeveloperEvaluation.Domain.Dtos.Sales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery
{
    public class GetSaleWithPaginationQueryResponse
    {
        public GetSaleWithPaginationQueryResponse() { }

        public GetSaleWithPaginationQueryResponse(IEnumerable<SalePaginationDto> data, int total)
        {
            Data = data;
            Total = total;
        }

        public IEnumerable<SalePaginationDto> Data { get; set; } = default!;
        public int Total { get; set; } = 0;
    }
}
