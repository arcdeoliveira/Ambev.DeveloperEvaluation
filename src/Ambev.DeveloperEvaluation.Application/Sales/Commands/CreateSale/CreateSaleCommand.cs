using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public int AfiliateId { get; set; }
        public Guid UserId { get; set; }    
        public IEnumerable<ProductSaleDto> Products { get; set; } = [];
    }
}
