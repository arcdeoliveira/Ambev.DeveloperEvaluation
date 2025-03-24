using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleCommand  : IRequest<UpdateSaleResult>
    {
        public string Id { get; set; } = string.Empty;
        public int? AffiliateId { get; set; }
        public Guid UserId { get; set; } = Guid.Empty;
        public IEnumerable<ProductSaleDto> NewProductIds { get; set; } = [];
    }
}
