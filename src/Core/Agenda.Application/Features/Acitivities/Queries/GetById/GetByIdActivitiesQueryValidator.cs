using Agenda.Application.Features.Activities.Queries.GetById;
using Agenda.Application.Features.Extension.Validation;
using FluentValidation;

namespace Agenda.Application.Features.Acitivities.Queries.GetById;

public class GetByIdActivitiesQueryValidator : AbstractValidator<GetByIdActivitiesQuery>
{
    public GetByIdActivitiesQueryValidator()
    {
        RuleFor(v => v.Id).ValidateId();
    }
}