using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.Pause;

public class PauseActivityCommandValidator : AbstractValidator<PauseActivityCommand>
{
    public PauseActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();
    }
}