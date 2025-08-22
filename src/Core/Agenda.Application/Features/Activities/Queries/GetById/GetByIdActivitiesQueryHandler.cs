using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetById;

public class GetByIdActivitiesQueryHandler : IRequestHandler<GetByIdActivitiesQuery, ActivityDTO?>
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetByIdActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<ActivityDTO?> Handle(GetByIdActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _activityReadRepository.GetById(request.Id);
    }
}