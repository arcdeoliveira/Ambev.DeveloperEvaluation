using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.Queries.GetProductByIdQuery
{
    public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
    {
        public GetProductByIdQueryValidator() 
        {
            
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Product ID is required");
        }
    }
}
