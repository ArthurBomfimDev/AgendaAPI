using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByDueDateRange;

public record GetByDueDateRangeActivitiesQuery(
    DateTimeOffset? DueDateFrom,
    DateTimeOffset? DueDateTo) : IRequest<IEnumerable<ActivityDTO>>;