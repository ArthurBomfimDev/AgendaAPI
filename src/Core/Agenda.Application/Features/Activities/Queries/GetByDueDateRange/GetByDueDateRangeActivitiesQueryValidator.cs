using Agenda.Application.Features.Activities.Queries.GetByDueDateRange;
using FluentValidation;

namespace Agenda.Application.Features.Acitivities.Queries.GetByDueDateRange;

public class GetByDueDateRangeActivitiesQueryValidator : AbstractValidator<GetByDueDateRangeActivitiesQuery>
{
    public GetByDueDateRangeActivitiesQueryValidator()
    {
        RuleFor(x => x.DueDateTo)
            .GreaterThanOrEqualTo(x => x.DueDateFrom)
            .WithMessage("A data final do filtro não pode ser anterior à data inicial.")
            .When(x => x.DueDateFrom.HasValue && x.DueDateTo.HasValue);
    }
}