using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale
{
    public class CancelSaleValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleValidator()
        {
            RuleFor(x => x.Id)
             .NotEmpty().WithMessage("Sale ID is required")
             .Must(id => Guid.TryParse(id, out _)).WithMessage("Invalid Sale ID.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid User ID.");
        }
    }
}
