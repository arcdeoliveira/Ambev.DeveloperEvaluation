using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleWithPaginationQuery
{
    public class GetSaleWithPaginationValidator : AbstractValidator<GetSaleWithPaginationQuery>
    {
        public GetSaleWithPaginationValidator() 
        {            
            RuleFor(x => x.PageNumber)
                .GreaterThan(0).WithMessage("Page number is required.");

            RuleFor(x => x.PageSize)
               .GreaterThan(0).WithMessage("Page size is required.");
        }
    }
}
