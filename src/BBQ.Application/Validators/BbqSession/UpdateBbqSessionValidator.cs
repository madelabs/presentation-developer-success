using FluentValidation;
using BBQ.Application.DTOs.BbqSession;

namespace BBQ.Application.Validators.BbqSession;

public class UpdateBbqSessionValidator : AbstractValidator<UpdateBbqSessionInputDto>
{
    public UpdateBbqSessionValidator()
    {
        RuleFor(ctl => ctl.Description)
            .MinimumLength(BbqSessionValidatorConfiguration.MinimumTitleLength)
            .WithMessage(
                $"BBQ Session description must contain a minimum of {BbqSessionValidatorConfiguration.MinimumTitleLength} characters")
            .MaximumLength(BbqSessionValidatorConfiguration.MaximumTitleLength)
            .WithMessage(
                $"BBQ Session description must contain a minimum of {BbqSessionValidatorConfiguration.MaximumTitleLength} characters");
    }
}
