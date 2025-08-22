using Agenda.Domain.Enuns.ActivityPriority;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.UpdatePriority;

public record UpdatePriorityActivityCommand(
    long Id,
    EnumActivityPriority Priority) : IRequest<Unit>;