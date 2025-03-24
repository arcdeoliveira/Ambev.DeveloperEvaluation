using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSaleByIdQuery
{
    public class GetSaleByIdQueryValidator : AbstractValidator<GetSaleByIdQuery>
    {
        public GetSaleByIdQueryValidator() 
        {

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .Must(x => Guid.TryParse(x, out _)).WithMessage("Sale ID is invalid.");
        }
    }
}
