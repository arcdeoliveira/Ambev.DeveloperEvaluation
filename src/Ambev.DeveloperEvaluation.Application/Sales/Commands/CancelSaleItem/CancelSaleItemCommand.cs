using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemCommand : IRequest<CancelSaleItemResult>
    {
        public string Id { get; set; }  = string.Empty; 
        public Guid UserId { get; set; } = Guid.Empty;
        public IEnumerable<string> ProductIds { get; set; } = [];

    }
}
