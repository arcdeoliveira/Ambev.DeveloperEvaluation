using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductWithPaginationQuery
{
    public class GetProductWithPaginationValidator : AbstractValidator<GetProductWithPaginationQuery>
    {
        public GetProductWithPaginationValidator() 
        {            
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page index is required");

            RuleFor(x => x.PageSize)
               .GreaterThan(0).WithMessage("Page size is required.");
        }
    }
}
