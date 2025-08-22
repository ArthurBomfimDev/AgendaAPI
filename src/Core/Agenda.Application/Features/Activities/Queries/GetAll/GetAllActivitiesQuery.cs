using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetAll;

public record GetAllActivitiesQuery() : IRequest<IEnumerable<ActivityDTO>>;