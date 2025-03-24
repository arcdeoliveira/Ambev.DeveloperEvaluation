using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleCommand  : IRequest<DeleteSaleResponse>
    {
        public DeleteSaleCommand(string id) 
        {
            Id = id;
        }

        public string Id { get; set; }  
        public Guid UserId { get; set; }    
    }
}
