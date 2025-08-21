using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByStatus;

public class GetByStatusActivitiesQueryHandler : IRequestHandler<GetByStatusActivitiesQuery, IEnumerable<ActivityDTO>>
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetByStatusActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IEnumerable<ActivityDTO>> Handle(GetByStatusActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _activityReadRepository.GetByStatus(request.Status);
    }
}