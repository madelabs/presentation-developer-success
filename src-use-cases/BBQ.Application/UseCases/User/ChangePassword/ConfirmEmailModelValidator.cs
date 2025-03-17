using FluentValidation;
using BBQ.Application.UseCases.User.ConfirmEmail;

namespace BBQ.Application.UseCases.User.ChangePassword;

public class ConfirmEmailModelValidator : AbstractValidator<ConfirmEmailInputDto>
{
    public ConfirmEmailModelValidator()
    {
        RuleFor(ce => ce.Token)
            .NotEmpty()
            .WithMessage("Your verification link is not valid");

        RuleFor(ce => ce.UserId)
            .NotEmpty()
            .WithMessage("Your verification link is not valid");
    }
}
