using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery
{
    public class GetProductWithPaginationQuery : IRequest<GetProductWithPaginationQueryResponse>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public DateTime? CreatedInitial { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; private set; }
        public ProductStatus? Status { get; private set; }
    }
}
