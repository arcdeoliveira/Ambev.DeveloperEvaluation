using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DiscontinueProduct
{
    public class DiscontinueProductRequestValidator : AbstractValidator<DiscontinueProductRequest>
    {
        public DiscontinueProductRequestValidator() 
        {
            RuleFor(x => x.Id)
           .NotEmpty()
           .WithMessage("User ID is required");
        }
    }
}
