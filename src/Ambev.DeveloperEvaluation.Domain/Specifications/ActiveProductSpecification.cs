using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Interfaces.Specifications;

namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public class ActiveProductSpecification : ISpecification<Product>
{
    public bool IsSatisfiedBy(Product product)
    {
        return product.Status == ProductStatus.Active;
    }
}
