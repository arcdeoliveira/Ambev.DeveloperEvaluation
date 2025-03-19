using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class AfiliateValidator : AbstractValidator<Afiliate>
    {
        public AfiliateValidator()
        {
            RuleFor(afiliate => afiliate.Name)
                .NotEmpty()
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Name cannot be longer than 50 characters.");

            RuleFor(afiliate => afiliate.Description)
                .MinimumLength(3).WithMessage("Description must be at least 3 characters long.")
                .MaximumLength(100).WithMessage("Description cannot be longer than 50 characters.");

            RuleFor(afiliate => afiliate.Cnpj)
                .GreaterThan(0).WithMessage("Cnpj must be informed.")
                .Must(CnpjIsValid).WithMessage("Cnpj is invalid.");


            RuleFor(afiliate => afiliate.Adress).SetValidator(new AdressValidator());  
        }


        private bool CnpjIsValid(int cnpj)
        {
            if (cnpj == 0)
                return false;

            var rule = "001";
            var cnpjString = cnpj.ToString();

            if(!cnpjString.Contains(rule)) 
                return false;

            return cnpjString.Length == 14;
        }
    }
}
