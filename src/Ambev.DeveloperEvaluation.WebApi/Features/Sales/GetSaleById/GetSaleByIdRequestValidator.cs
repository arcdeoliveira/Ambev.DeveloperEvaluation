using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdRequestValidator : AbstractValidator<GetSaleByIdRequest>
    {
        public GetSaleByIdRequestValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.")
                .Must(x => Guid.TryParse(x, out _)).WithMessage("Sale ID is invalid.");
        }
    }
}
