using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class ProductSale : BaseMongoDBEntity
    {
        public ProductSale() { }
        
        public ProductSale(decimal unitPrice, decimal unitDiscount, int quantity, decimal total, int productId)
        {
            UnitPrice = unitPrice;
            UnitDiscount = unitDiscount; 
            Quantity = quantity;
            Total = total; 
            ProductId = productId;
        }

        public decimal UnitPrice { get; private set; } = 0;
        public decimal UnitDiscount { get; private set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get; private set; } = 0;
        public int ProductId { get; private set; } = 0;


        public void AlterUnitPrice(decimal unitPrice)
        {
            UnitPrice = unitPrice;
        }

        public void AlterUnitDiscount(decimal unitDiscount)
        {
            UnitDiscount = unitDiscount;
        }

        public void AlterQuantity(int quantity)
        {
            Quantity = quantity;
        }

        public void AlterTotal(decimal total)
        {
            Total = total;
        }

        public void AlterProduct(int productId)
        {
            ProductId = productId;
        }
    }
}
