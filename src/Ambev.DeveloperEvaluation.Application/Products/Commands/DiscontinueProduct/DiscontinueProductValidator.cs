using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DiscontinueProduct
{
    public class DiscontinueProductValidator : AbstractValidator<DiscontinueProductCommand>
    {
        public DiscontinueProductValidator()
        {
            RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage("Product Id is required");
        }
    }
}
