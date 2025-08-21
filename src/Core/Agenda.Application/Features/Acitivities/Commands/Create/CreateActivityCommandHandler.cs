using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Application.Interfaces.UnitOfWork;
using Agenda.Domain.Entities;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Create;

public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, long>
{
    private readonly IActivityRepository _activityRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork)
    {
        _activityRepository = activityRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<long> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
    {
        var acitivity = Activity.Create(request.Title, request.Description, request.Priority, request.DueDate);

        await _activityRepository.Create(acitivity);
        await _unitOfWork.SaveChangesAsync();

        return acitivity.Id;
    }
}