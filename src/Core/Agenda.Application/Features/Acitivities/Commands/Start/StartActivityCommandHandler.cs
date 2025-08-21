using Agenda.Application.Common.Exceptions;
using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Application.Interfaces.UnitOfWork;
using Agenda.Domain.Entities;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Start;

public class StartActivityCommandHandler : IRequestHandler<StartActivityCommand, Unit>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public StartActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(StartActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.GetById(request.Id);

        if (activity == null)
            throw new NotFoundException(nameof(Activity), request.Id);

        activity.Start();

        _activityRepository.Update(activity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}