using MediatR;

namespace Agenda.Application.Features.Acitivities.Commands.UpdateDueDate;

public record UpdateDueDateActivityCommand(
    long Id,
    DateTimeOffset? DueDate) : IRequest<Unit>;