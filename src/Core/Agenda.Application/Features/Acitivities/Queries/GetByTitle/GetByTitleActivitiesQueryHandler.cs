using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByTitle;

public class GetByTitleActivitiesQueryHandler : IRequestHandler<GetByTitleActivitiesQuery, IEnumerable<ActivityDTO>>
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetByTitleActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IEnumerable<ActivityDTO>> Handle(GetByTitleActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _activityReadRepository.GetByTitle(request.Title);
    }
}