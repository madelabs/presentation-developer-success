using FluentValidation;

namespace BBQ.Application.UseCases.BbqSession.UpdateBbqSession;

public class UpdateBbqSessionValidator : AbstractValidator<UpdateBbqSessionCommand>
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
