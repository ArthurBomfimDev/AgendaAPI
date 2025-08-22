using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Commands.Update;

public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
{
    public UpdateActivityCommandValidator()
    {
        RuleFor(v => v.Id).ValidateId();

        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título não pode exceder 100 caracteres.");

        RuleFor(v => v.Description)
            .MaximumLength(500).WithMessage("A descrição não pode exceder 500 caracteres.");

        RuleFor(v => v.Priority)
            .NotEmpty().WithMessage("O nivel de prioridade é obrigatorio")
            .IsInEnum().WithMessage("A prioridade da tarefa é um campo obrigatorio. 1 -> Baixa Prioridade | 2 -> Média Prioridade | 3 -> Alta Prioridade ");

        RuleFor(v => v.DueDate)
            .GreaterThanOrEqualTo(DateTimeOffset.UtcNow.Date)
            .WithMessage("A data de vencimento não pode ser uma data no passado.")
            .When(v => v.DueDate.HasValue);
    }
}