namespace yourmomENT.Validators;

using FluentValidation;
using yourmomENT.Dto;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
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