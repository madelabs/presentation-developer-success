using FluentValidation;
using BBQ.Application.DTOs.SessionNote;

namespace BBQ.Application.Validators.SessionNote;

public class UpdateSessionNoteModelValidator : AbstractValidator<UpdateSessionNoteInputDto>
{
    public UpdateSessionNoteModelValidator()
    {
        RuleFor(cti => cti.Note)
            .MinimumLength(SessionNoteValidatorConfiguration.MinimumTitleLength)
            .WithMessage(
                $"Session Note should have minimum ${SessionNoteValidatorConfiguration.MaximumTitleLength} characters")
            .MaximumLength(SessionNoteValidatorConfiguration.MaximumTitleLength)
            .WithMessage(
                $"Session Note should have maximum {SessionNoteValidatorConfiguration.MaximumTitleLength} characters");

        RuleFor(cti => cti.ActivityDescription)
            .MinimumLength(SessionNoteValidatorConfiguration.MinimumBodyLength)
            .WithMessage(
                $"Session Note activity description should have minimum {SessionNoteValidatorConfiguration.MinimumBodyLength} characters")
            .MaximumLength(SessionNoteValidatorConfiguration.MaximumBodyLength)
            .WithMessage(
                $"Session Note activity description should have maximum {SessionNoteValidatorConfiguration.MaximumBodyLength} characters");
    }
}
