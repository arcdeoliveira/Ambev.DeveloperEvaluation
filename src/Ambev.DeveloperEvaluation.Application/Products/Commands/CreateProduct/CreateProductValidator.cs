using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator() 
        {
            RuleFor(product => product.Name)
                .NotEmpty().WithMessage("Product name must be informed")
                .MinimumLength(3).WithMessage("Product Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Product Name cannot be longer than 100 characters.");

            RuleFor(product => product.Description)
                .MaximumLength(250).WithMessage("Product Description cannot be longer than 250 characters.");

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Product price must be informed.");

            RuleFor(product => product.UserId)
                .NotEqual(Guid.Empty).WithMessage("User must be logged in");    
        }
    }
}
