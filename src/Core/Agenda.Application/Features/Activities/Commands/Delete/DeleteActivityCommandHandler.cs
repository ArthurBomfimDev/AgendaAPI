using Agenda.Application.Common.Exceptions;
using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Application.Interfaces.UnitOfWork;
using Agenda.Domain.Entities;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Delete;

public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Unit>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
    {
        var activity = await _activityRepository.GetById(request.Id);

        if (activity == null)
            throw new NotFoundException(nameof(Activity), request.Id);

        _activityRepository.Delete(activity);
        await _unitOfWork.SaveChangesAsync();

        return Unit.Value;
    }
}