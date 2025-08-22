using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.Start;

public class StartActivityCommandValidator : AbstractValidator<StartActivityCommand>
{
    public StartActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();
    }
}