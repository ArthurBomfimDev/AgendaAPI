using Agenda.Application.ViewModels.DTO.Activity;
using Agenda.Domain.Enuns.ActivityStatus;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByStatus;

public record GetByStatusActivitiesQuery(EnumActivityStatus Status) : IRequest<IEnumerable<ActivityDTO>>;