namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class ProductSale
    {
        public ProductSale() { }
        
        public ProductSale(decimal unitPrice, int quantity, string productId)
        {
            UnitPrice = unitPrice;
            Quantity = quantity;
            ProductId = productId;
        }

        public string ProductId { get; private set; } = string.Empty;
        public decimal UnitPrice { get; private set; } = 0;
        public decimal UnitDiscount { get; private set; } = 0;
        public int Quantity { get; set; } = 0;
        public decimal Total { get; private set; } = 0;
        public bool Canceled { get; private set; }
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? DateCanceled { get; private set; } 


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

        public void AlterProduct(string productId)
        {
            ProductId = productId;
        }

        public void SetDiscountByQuantity()
        {
            var percentage = Quantity switch
            {
                <= 4 => decimal.Zero,
                <= 10 => 10,
                <= 20 => 20,
                _ => decimal.Zero,
            };

            if (percentage == decimal.Zero)
            {
                UnitDiscount = decimal.Zero;
                return;
            }

            var discount = UnitPrice * (percentage / 100);  
            UnitDiscount = Math.Round(discount, 2, MidpointRounding.ToEven);
        }

        public void SetTotal()
        {
            var total = (UnitPrice - UnitDiscount) * Quantity;
            Total = Math.Round(total, 2, MidpointRounding.ToEven);
        }   

        public void Cancel()
        {
            Canceled = true;
            DateCanceled = DateTime.UtcNow;
        }

    }
}
