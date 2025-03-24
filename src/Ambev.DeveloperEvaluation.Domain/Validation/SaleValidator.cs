using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleValidator : AbstractValidator<Sale>
    {
        public SaleValidator() 
        {
            RuleFor(sale => sale.CreatedAt)
                .NotEqual(DateTime.MinValue).WithMessage("Date of creation must be informed.")
                .NotEqual(DateTime.MaxValue).WithMessage("Date of creation must be informed.");

            RuleFor(sale => sale.Status).
                NotEqual(SaleStatus.None).WithMessage("Status must be informed.");

            RuleFor(sale => sale.Total)
                .GreaterThan(0).WithMessage("Total must be informed.");
          
         
            RuleFor(sale => sale.AfiliateId)
                .GreaterThan(0).WithMessage("AfiliateId must be informed.");

            RuleFor(sale => sale.UserId)
                .NotEmpty().WithMessage("UserId must be informed.")
                .NotEqual(Guid.Empty).WithMessage("UserId must be informed.");

            RuleFor(sale => sale.ProductSales)
                .NotEqual([]).WithMessage("Products to sale must be informed.");

            RuleForEach(x => x.ProductSales).SetValidator(new ProductSaleValidator());  
        }  
    }
}
