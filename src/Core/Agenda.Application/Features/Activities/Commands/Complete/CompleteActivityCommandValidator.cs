using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.Complete;

public class CompleteActivityCommandValidator : AbstractValidator<CompleteActivityCommand>
{
    public CompleteActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();
    }
}