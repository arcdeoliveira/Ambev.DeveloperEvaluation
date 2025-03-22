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
                .NotEmpty().WithMessage("Product name must be informed.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 100 characters.");

            RuleFor(product => product.Description)
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .MaximumLength(250).WithMessage("Description cannot be longer than 250 characters."); 

            RuleFor(product => product.Price)
                .GreaterThan(0).WithMessage("Price must be informed.")
                .LessThanOrEqualTo(10000).WithMessage("Maximum price 10.000,00");   

            RuleFor(product => product.Status)
                .NotEqual(ProductStatus.Unknown).WithMessage("Product status must be informed.");
        }   
    }
}
