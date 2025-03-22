using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand  : IRequest<DeleteProductResponse>
    {
        public DeleteProductCommand(string id) 
        {
            Id = id;
        }

        public string Id { get; set; }  
    }
}
