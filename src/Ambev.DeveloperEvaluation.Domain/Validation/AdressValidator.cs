using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class AdressValidator : AbstractValidator<Adress>
    {
        public AdressValidator()
        {
            RuleFor(adress => adress.Street)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Street must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Street cannot be longer than 50 characters.");

            RuleFor(adress => adress.Number)
                .GreaterThan(0).WithMessage("Number must be greater than 0.");

            RuleFor(adress => adress.Complement)
                .MinimumLength(3).WithMessage("Complement must be at least 3 characters long.")
                .MaximumLength(50).WithMessage("Complement cannot be longer than 50 characters.");

            RuleFor(adress => adress.Afiliate).SetValidator(new AfiliateValidator());

            RuleFor(adress => adress.AfiliateId)
                .Equal(adress => adress.Afiliate.Id).WithMessage("AfiliateId must be equal to Afiliate from relationship.");   
        }
      
    }
}
