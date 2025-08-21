using Agenda.Domain.Enuns.ActivityPriority;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Update;

public record UpdateActivityCommand(
    long Id,
    string Title,
    string? Description,
    EnumActivityPriority Priority,
    DateTimeOffset? DueDate
    ) : IRequest<Unit>;