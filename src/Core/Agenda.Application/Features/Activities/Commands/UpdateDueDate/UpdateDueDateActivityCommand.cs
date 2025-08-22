using MediatR;

namespace Agenda.Application.Features.Activities.Commands.UpdateDueDate;

public record UpdateDueDateActivityCommand(
    long Id,
    DateTimeOffset? DueDate) : IRequest<Unit>;