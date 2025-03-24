using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator() 
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("Sale ID is required");

            RuleFor(x => x.AfiliateId)
                .GreaterThan(0).When(x => x.AfiliateId.HasValue).WithMessage("Invalid Affiliate ID.");

            RuleFor(x => x.NewProductIds)
                .NotEqual([]).WithMessage("Products are required.")
                .Must(ProductSaleDto.AllProductIdsValids).WithMessage(x => ProductSaleDto.GetInvalidProductIdsMessage(x.NewProductIds))
                .Must(ProductSaleDto.AllProductQuantityValid).WithMessage(x => ProductSaleDto.GetInvalidProductQuantitiesMessage(x.NewProductIds));
        }
    }
}
