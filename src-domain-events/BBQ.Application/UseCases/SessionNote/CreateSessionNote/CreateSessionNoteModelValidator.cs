﻿using FluentValidation;

namespace BBQ.Application.UseCases.SessionNote.CreateSessionNote;

public class CreateSessionNoteValidator : AbstractValidator<CreateSessionNoteCommand>
{
    public CreateSessionNoteValidator()
    {
        RuleFor(cti => cti.Note)
            .MinimumLength(SessionNoteValidatorConfiguration.MinimumTitleLength)
            .WithMessage(
                $"Session Note should have minimum {SessionNoteValidatorConfiguration.MinimumTitleLength} characters")
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
