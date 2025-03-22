using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreateProductResult>
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; } = 0;
        public Guid UserId { get; set; } = Guid.Empty;  
    }
}
