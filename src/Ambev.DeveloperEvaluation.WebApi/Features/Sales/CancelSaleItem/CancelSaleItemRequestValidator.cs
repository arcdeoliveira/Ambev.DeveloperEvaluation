using System.Text;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItem
{
    public class CancelSaleItemRequestValidator : AbstractValidator<CancelSaleItemRequest>
    {
        public CancelSaleItemRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required");

            RuleFor(x => x.ProductIds)
                .NotEqual([]).WithMessage("Product ID(s) must be informed.")
                .Must(Verify).WithMessage(x => CreateInvalidProductIdsMessage(x.ProductIds));
        }

        private bool Verify(IEnumerable<string> productIds)
        {
            return productIds.Any(x => !string.IsNullOrEmpty(x));
        }

        private static string CreateInvalidProductIdsMessage(IEnumerable<string> productIds)
        {
            var stringBuider = new StringBuilder(string.Empty);

            return productIds.Aggregate(stringBuider, (current, productId) =>
            {
                if (string.IsNullOrEmpty(productId))
                    current.AppendLine($"Product ID {productId} is invalid.");
                return current;
            }).ToString();
        }
    }
}
