using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator : AbstractValidator<Product>  
    {
        public ProductValidator() 
        { 
            RuleFor(product => product.Name)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(product => product.Description)
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Description cannot be longer than 50 characters."); 

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be informed.");   

            RuleFor(product => product.Status)
                .NotEqual(ProductStatus.Unknown)
                .WithMessage("Product status must be informed.");
        }   
    }
}
