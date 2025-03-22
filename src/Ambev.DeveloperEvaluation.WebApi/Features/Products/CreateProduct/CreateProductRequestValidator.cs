using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
    {
        public CreateProductRequestValidator() 
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Product name must be informed.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters."); 

            RuleFor(product => product.Description)
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .MaximumLength(250).WithMessage("Description cannot be longer than 250 characters.");

            RuleFor(product => product.Price)
                .NotNull().WithMessage("Price must be informed.")
                .GreaterThan(0).WithMessage("Price must be informed.")
                .LessThanOrEqualTo(10000).WithMessage("Maximum price 10.000,00");
        }
    }
}
