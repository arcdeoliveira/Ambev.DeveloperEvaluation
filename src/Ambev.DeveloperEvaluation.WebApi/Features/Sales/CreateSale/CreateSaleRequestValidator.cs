using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator() 
        {
            RuleFor(x => x.AfiliateId)
                .NotNull().WithMessage("Affiliate ID is required.")
                .GreaterThan(0).When(x => x.AfiliateId.HasValue).WithMessage("Invalid Affiliate ID.");

            RuleFor(x => x.Products)
                .NotEqual([]).WithMessage("Products are required.")
                .Must(ProductSaleDto.AllProductIdsValids).WithMessage(x => ProductSaleDto.GetInvalidProductIdsMessage(x.Products))
                .Must(ProductSaleDto.AllProductQuantityValid).WithMessage(x => ProductSaleDto.GetInvalidProductQuantitiesMessage(x.Products));
        }
    }
}
