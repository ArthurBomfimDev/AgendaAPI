using Agenda.Application.Features.Activities.Queries.GetByTitle;
using FluentValidation;

namespace Agenda.Application.Features.Activities.Queries.GetByTitle;

partial class GetByTitleActivitiesQueryValidator : AbstractValidator<GetByTitleActivitiesQuery>
{
    public GetByTitleActivitiesQueryValidator()
    {
        RuleFor(v => v.Title)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .MaximumLength(100).WithMessage("O título não pode exceder 100 caracteres.");
    }
}