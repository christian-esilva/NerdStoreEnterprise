using FluentValidation;
using NSE.Core.DomainObjects;
using NSE.Core.Utils;

namespace NSE.Customers.API.Application.Commands.Validations
{
    public class RegisterCustomerValidation : AbstractValidator<RegisterCustomerCommand>
    {
        public RegisterCustomerValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("O Id do cliente não é válido");
            
            RuleFor(c => c.Name)
                .NotEmpty()
                .MaximumLength(200)
                .WithMessage("O Nome informado não é válido");

            RuleFor(c => c.Cpf)
                .Must(CpfIsValid)
                .WithMessage("O Cpf informado não é válido");

            RuleFor(c => c.Email)
                .Must(EmailIsValid)
                .WithMessage("O Email informado não é válido");
        }

        protected static bool CpfIsValid(string cpf)
        {
            return Cpf.Validate(cpf);
        }

        protected static bool EmailIsValid(string email)
        {
            return Email.Validate(email);
        }
    }
}
