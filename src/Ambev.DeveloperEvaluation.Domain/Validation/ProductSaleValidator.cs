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
                .GreaterThanOrEqualTo(0).When(x => x.Quantity > 0).WithMessage("Discount must be informed.");
            
            RuleFor(productSale => productSale.Total)
                .GreaterThan(0).When(x => x.UnitPrice > decimal.Zero && x.UnitDiscount >= decimal.Zero).WithMessage("Total must be informed.");

            RuleFor(productSale => productSale.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be informed.")
                .LessThanOrEqualTo(20).WithMessage("The maximum for quantity is 20 items");

            RuleFor(productSale => productSale.ProductId)
                .NotNull().WithMessage("ProductId must be informed.");

            RuleFor(productSale => productSale.DateCanceled)
                .NotNull().When(x => x.Canceled).WithMessage("Date of cancellation must be informed when is canceled.");
        }
    }
}
