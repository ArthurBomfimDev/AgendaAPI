using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetById;

public record GetByIdActivitiesQuery(long Id) : IRequest<ActivityDTO?>;