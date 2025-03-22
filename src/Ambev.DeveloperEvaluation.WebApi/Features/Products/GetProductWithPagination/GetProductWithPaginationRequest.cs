using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.WebApi.Common.Request;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductWithPagination
{
    public class GetProductWithPaginationRequest : BasePaginationRequest
    {
        public DateTime? CreatedInitial { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public ProductStatus? Status { get; set; } 

    }
}
