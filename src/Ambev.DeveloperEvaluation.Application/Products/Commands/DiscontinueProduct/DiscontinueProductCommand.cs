using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DiscontinueProduct
{
    public class DiscontinueProductCommand  : IRequest<DiscontinueProductResponse>
    {
        public DiscontinueProductCommand(string id) 
        {
            Id = id;
        }

        public string Id { get; set; }  
    }
}
