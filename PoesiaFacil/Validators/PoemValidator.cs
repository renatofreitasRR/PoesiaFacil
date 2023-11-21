using FluentValidation;
using PoesiaFacil.Entities;
using PoesiaFacil.Validators.Messages;

namespace PoesiaFacil.Validators
{
    public class PoemValidator : AbstractValidator<Poem>
    {
        public PoemValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty()
                .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Poema"));

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage(ValidatorMessages.FieldIsEmptyMessage("Título"));
        }
    }
}
