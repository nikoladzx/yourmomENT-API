namespace yourmomENT.Validators;

using FluentValidation;
using yourmomENT.DTO;

public class RegisterDtoValidator : AbstractValidator<RegisterDTO>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(x => x.Password)
            .MinimumLength(8);

        RuleFor(x => x.Username)
            .NotEmpty()
            .MinimumLength(8);
    }
}