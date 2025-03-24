using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleCommand  : IRequest<CancelSaleResponse>
    {
        public CancelSaleCommand(string id) 
        {
            Id = id;
        }

        public string Id { get; set; }
        public Guid UserId { get; set; }
    }
}
