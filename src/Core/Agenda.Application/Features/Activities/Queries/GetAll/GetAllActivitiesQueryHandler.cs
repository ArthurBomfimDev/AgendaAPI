using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.ViewModels.DTO.Activity;
using MediatR;

namespace Agenda.Application.Features.Activities.Queries.GetAll;

public class GetAllActivitiesQueryHandler : IRequestHandler<GetAllActivitiesQuery, IEnumerable<ActivityDTO>>
{
    private readonly IActivityReadRepository _activityReadRepository;

    public GetAllActivitiesQueryHandler(IActivityReadRepository activityReadRepository)
    {
        _activityReadRepository = activityReadRepository;
    }

    public async Task<IEnumerable<ActivityDTO>> Handle(GetAllActivitiesQuery request, CancellationToken cancellationToken)
    {
        return await _activityReadRepository.GetAll();
    }
}