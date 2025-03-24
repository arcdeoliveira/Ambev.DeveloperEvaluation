using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale
{
    public class CreateSaleValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleValidator() 
        {
            RuleFor(x => x.AfiliateId)
                 .GreaterThan(0).WithMessage("Affiliate ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid User ID.");

            RuleFor(x => x.Products)
                .NotEqual([]).WithMessage("Products are required.")
                .Must(ProductSaleDto.AllProductIdsValids).WithMessage(x => ProductSaleDto.GetInvalidProductIdsMessage(x.Products))
                .Must(ProductSaleDto.AllProductQuantityValid).WithMessage(x => ProductSaleDto.GetInvalidProductQuantitiesMessage(x.Products));
        }
    }
}
