using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByTitle;

public record GetByTitleActivitiesQuery(string Title) : IRequest<IEnumerable<ActivityDTO>>;