using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseDocument
    {
        public Sale() { }

        public Sale(SaleStatus status, decimal total, Guid userId, short afiliateId)
        {
            Status = status;
            Total = total;
            UserId = userId;
            AfiliateId = afiliateId;
        }

        public SaleStatus Status { get; private set; } = SaleStatus.Ordered;
        public decimal Total { get; private set; } = 0;

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid UserId { get; private set; } = Guid.Empty;
        public int AfiliateId { get; private set; } = 0;

        public IEnumerable<ProductSale> ProductSales { get; private set; } = [];


        public void AlterStatus(SaleStatus status)
        {
            Status = status;
        }  

        public void AlterCostumer(Guid userId)
        {
            UserId = userId;          
        }

        public void AlterAffiliate(short afiliateId)
        {
            AfiliateId = afiliateId;
        }

        public void AlterItens(IEnumerable<ProductSale> productSales)
        {
            ProductSales = productSales;
        }   

        public void SetTotal()
        {
            Total = ProductSales.Where(w => w.Canceled == false).Sum(x => x.Total);
        }
    }
}
