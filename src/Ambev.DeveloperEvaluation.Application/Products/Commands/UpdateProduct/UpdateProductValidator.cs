using Ambev.DeveloperEvaluation.Domain.Enums;
using System.Collections.ObjectModel;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
              .NotEmpty()
              .WithMessage("Product ID is required");

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
            .NotEqual(ProductStatus.Unknown).WithMessage("Product status must be informed.")
            .Must(IsProductActived)
            .WithMessage("For update status to discontiued or inactive, use the respective endpoint acess");
        }

        private bool IsProductActived(ProductStatus productStatus)
        {
            var statusProibited = new Collection<ProductStatus> { ProductStatus.Unknown, ProductStatus.Discontinued, ProductStatus.Inactive };

            return !statusProibited.Contains(productStatus);
        }
    }
}
