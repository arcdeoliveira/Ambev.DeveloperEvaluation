namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemRequest
    {
        public string Id { get; set; }  = string.Empty; 
        public IEnumerable<string> ProductIds { get; set; } = [];
    }
}
