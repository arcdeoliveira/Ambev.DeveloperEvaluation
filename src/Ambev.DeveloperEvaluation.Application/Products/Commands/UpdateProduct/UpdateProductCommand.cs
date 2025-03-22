using Ambev.DeveloperEvaluation.Domain.Enums;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand  : IRequest<UpdateProductResult>
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty;
        public decimal Price { get; private set; } = decimal.Zero;
        public ProductStatus Status { get; private set; } = ProductStatus.Unknown;
    }
}
