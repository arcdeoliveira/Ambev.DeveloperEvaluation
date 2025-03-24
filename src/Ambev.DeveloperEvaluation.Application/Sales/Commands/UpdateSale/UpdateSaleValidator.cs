using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Dtos.ProductSales;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale
{
    public class UpdateSaleValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleValidator()
        {
            RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Sale ID is required")
            .Must(IsValidGuid).WithMessage("Invalid Sale ID.");

            RuleFor(x => x.AffiliateId)
                .GreaterThan(0).When(x => x.AffiliateId.HasValue).WithMessage("Invalid Affiliate ID.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required")
                .NotEqual(Guid.Empty).WithMessage("Invalid User ID.");

            RuleFor(x => x.NewProductIds)
                .NotEqual([]).WithMessage("Products are required.")
                .Must(ProductSaleDto.AllProductIdsValids).WithMessage(x => ProductSaleDto.GetInvalidProductIdsMessage(x.NewProductIds))
                .Must(ProductSaleDto.AllProductQuantityValid).WithMessage(x => ProductSaleDto.GetInvalidProductQuantitiesMessage(x.NewProductIds));
        }

        private bool IsValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
