using Agenda.Domain.Enuns.ActivityPriority;
using MediatR;

namespace Agenda.Application.Features.Acitivities.Commands.UpdatePriority;

public record UpdatePriorityActivityCommand(
    long Id,
    EnumActivityPriority Priority) : IRequest<Unit>;