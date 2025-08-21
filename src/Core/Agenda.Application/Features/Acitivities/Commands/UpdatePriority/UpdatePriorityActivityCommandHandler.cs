using Agenda.Application.Common.Exceptions;
using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Application.Interfaces.UnitOfWork;
using Agenda.Domain.Entities;
using MediatR;

namespace Agenda.Application.Features.Acitivities.Commands.UpdatePriority;

public class UpdatePriorityActivityCommandHandler : IRequestHandler<UpdatePriorityActivityCommand, Unit>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePriorityActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(UpdatePriorityActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.GetById(request.Id);

        if (activity == null)
            throw new NotFoundException(nameof(Activity), request.Id);

        activity.UpdatePriority(request.Priority);

        _activityRepository.Update(activity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}