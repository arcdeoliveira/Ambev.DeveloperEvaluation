using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductSaleValidator : AbstractValidator<ProductSale>
    {
        public ProductSaleValidator()
        {
            RuleFor(productSale => productSale.UnitPrice)
                .GreaterThan(0).WithMessage("Price must be informed.");

            RuleFor(productSale => productSale.UnitDiscount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount must be informed.");
            
            RuleFor(productSale => productSale.Total)
                .GreaterThan(0).WithMessage("Total must be informed.");

            RuleFor(productSale => productSale.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be informed.")
                .LessThanOrEqualTo(1000).WithMessage("The maximum for quantity is 1.000 items");

            RuleFor(productSale => productSale.ProductId)
                .NotNull().WithMessage("ProductId must be informed.")
                .Must(x => Guid.TryParse(x, out _)).WithMessage("ProductId is invalid.");
        }
    }
}
