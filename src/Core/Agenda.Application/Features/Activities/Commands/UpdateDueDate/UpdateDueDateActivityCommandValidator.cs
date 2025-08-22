using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Acitivities.Commands.UpdateDueDate;

public class UpdateDueDateActivityCommandValidator : AbstractValidator<UpdateDueDateActivityCommand>
{
    public UpdateDueDateActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();

        RuleFor(v => v.DueDate)
            .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Date)
            .WithMessage("A data de vencimento não pode ser uma data no passado.")
            .When(v => v.DueDate.HasValue);
    }
}