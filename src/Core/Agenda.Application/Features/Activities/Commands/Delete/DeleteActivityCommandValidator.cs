using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandValidator : AbstractValidator<DeleteActivityCommand>
{
    public DeleteActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();
    }
}