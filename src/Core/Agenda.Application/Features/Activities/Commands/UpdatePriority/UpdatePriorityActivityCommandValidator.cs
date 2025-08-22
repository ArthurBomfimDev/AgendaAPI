using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.UpdatePriority;

public class UpdatePriorityActivityCommandValidator : AbstractValidator<UpdatePriorityActivityCommand>
{
    public UpdatePriorityActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();

        RuleFor(v => v.Priority)
            .NotEmpty().WithMessage("O nivel de prioridade é obrigatorio")
            .IsInEnum().WithMessage("A prioridade da tarefa é um campo obrigatorio. 1 -> Baixa Prioridade | 2 -> Média Prioridade | 3 -> Alta Prioridade ");
    }
}