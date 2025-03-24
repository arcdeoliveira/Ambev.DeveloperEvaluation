using System.Text.Json.Serialization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemRequest
    {
        public CancelSaleItemRequest(string id, IEnumerable<string> productIds)
        {
            Id = id;
            ProductIds = productIds;
        }

        public string Id { get; set; }  = string.Empty; 
        public IEnumerable<string> ProductIds { get; set; } = [];
    }
}
