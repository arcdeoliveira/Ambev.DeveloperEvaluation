using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product : BaseDocument
    {
        public Product() { }    

        public Product(string name, string description, decimal price, ProductStatus status)
        {
            Name = name;
            Description = description;
            Price = price;
            Status = status;
        }

        public string Name { get; private set; } = string.Empty;
        public string Description { get; private set; } = string.Empty; 
        public decimal Price { get; private set; } = 0; 
        public ProductStatus Status { get; private set; } = ProductStatus.Active;


        public void AlterName(string name)
        {
            Name = name;
        }

        public void AlterDescription(string description)
        {
            Description = description;
        }   

        public void AlterPrice(decimal price)
        {
            Price = price;
        }

        public void AlterStatus(ProductStatus status)
        {
            Status = status;
        }
    }
}
