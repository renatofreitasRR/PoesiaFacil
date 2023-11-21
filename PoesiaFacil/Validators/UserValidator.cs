using FluentValidation;
using PoesiaFacil.Entities;
using PoesiaFacil.Validators.Messages;

namespace PoesiaFacil.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Nome"));

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Email"))
                .EmailAddress()
                .WithMessage(ValidatorMessages.FieldIsInAnInvalidFormat("Email"));

            RuleFor(x => x.Description)
              .NotEmpty()
              .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Descrição"));

            RuleFor(x => x.Password)
             .NotEmpty()
             .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Senha"));
        }
    }
}
