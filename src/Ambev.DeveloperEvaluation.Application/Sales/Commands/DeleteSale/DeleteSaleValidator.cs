using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale
{
    public class DeleteSaleValidator : AbstractValidator<DeleteSaleCommand>
    {
        public DeleteSaleValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty().WithMessage("Sale ID is required.");

            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User ID is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid User ID.");
        }
    }
}
