using Agenda.Application.Features.Activities.Queries.GetByStatus;
using FluentValidation;

namespace Agenda.Application.Features.Acitivities.Queries.GetByStatus;

public class GetByStatusActivitiesQueryValidator : AbstractValidator<GetByStatusActivitiesQuery>
{
    public GetByStatusActivitiesQueryValidator()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("A prioridade da tarefa é um campo obrigatorio. 1 -> Baixa Prioridade | 2 -> Média Prioridade | 3 -> Alta Prioridade ");
    }
}