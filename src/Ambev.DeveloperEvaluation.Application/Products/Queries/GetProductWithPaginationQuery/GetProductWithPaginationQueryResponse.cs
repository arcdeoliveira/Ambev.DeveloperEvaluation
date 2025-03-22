using Ambev.DeveloperEvaluation.Domain.Dtos.Products;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery
{
    public class GetProductWithPaginationQueryResponse
    {
        public GetProductWithPaginationQueryResponse() { }

        public GetProductWithPaginationQueryResponse(IEnumerable<ProductPaginationDto> data, int total)
        {
            Data = data;
            Total = total;
        }

        public IEnumerable<ProductPaginationDto> Data { get; set; } = default!;
        public int Total { get; set; } = 0;
    }
}
