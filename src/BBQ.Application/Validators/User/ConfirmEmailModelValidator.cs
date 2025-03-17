using FluentValidation;
using BBQ.Application.DTOs.User;

namespace BBQ.Application.Validators.User;

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
