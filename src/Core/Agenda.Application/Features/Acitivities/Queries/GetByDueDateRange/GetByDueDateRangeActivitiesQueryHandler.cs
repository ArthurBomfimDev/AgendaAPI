using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetByDueDateRange;

public class GetByDueDateRangeActivitiesQueryHandler : IRequestHandler<GetByDueDateRangeActivitiesQuery, IEnumerable<ActivityDTO>>
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetByDueDateRangeActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IEnumerable<ActivityDTO>> Handle(GetByDueDateRangeActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _activityReadRepository.GetByDueDateRange(request.DueDateFrom, request.DueDateTo);
    }
}