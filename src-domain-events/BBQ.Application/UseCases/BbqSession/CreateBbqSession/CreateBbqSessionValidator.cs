using FluentValidation;

namespace BBQ.Application.UseCases.BbqSession.CreateBbqSession;

public class CreateBbqSessionValidator : AbstractValidator<CreateBbqSessionCommand>
{
    public CreateBbqSessionValidator()
    {
        RuleFor(ctl => ctl.Description)
            .MinimumLength(BbqSessionValidatorConfiguration.MinimumTitleLength)
            .WithMessage(
                $"BBQ Session description must contain a minimum of {BbqSessionValidatorConfiguration.MinimumTitleLength} characters")
            .MaximumLength(BbqSessionValidatorConfiguration.MaximumTitleLength)
            .WithMessage(
                $"BBq Session description must contain a maximum of {BbqSessionValidatorConfiguration.MaximumTitleLength} characters");
    }
}
