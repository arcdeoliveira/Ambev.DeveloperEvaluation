using System.Text;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSaleItem
{
    public class CancelSaleItemValidator : AbstractValidator<CancelSaleItemCommand>
    {
        public CancelSaleItemValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .Must(x => Guid.TryParse(x, out _)).WithMessage("Invalid Sale ID.");

            RuleFor(x => x.ProductIds)
                .NotEqual([]).WithMessage("Product ID(s) must be informed.")
                .Must(Verify).WithMessage(x => CreateInvalidProductIdsMessage(x.ProductIds));

            RuleFor(x => x.UserId)
               .NotEmpty().WithMessage("User ID is required.")
               .NotEqual(Guid.Empty).WithMessage("Invalid User ID.");
        }

        private bool Verify(IEnumerable<string> productIds)
        {
            return productIds.All(x => Guid.TryParse(x, out _));
        }

        private static string CreateInvalidProductIdsMessage(IEnumerable<string> productIds)
        {
            var stringBuider = new StringBuilder(string.Empty);

            return productIds.Aggregate(stringBuider, (current, productId) =>
            {
                if (Guid.TryParse(productId, out _))
                    current.AppendLine($"Product ID {productId} is invalid.");
                return current;
            }).ToString();
        }
    }
}
