using Agenda.Domain.Enuns.ActivityPriority;
using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Create;

public record CreateActivityCommand(
    string Title,
    string? Description,
    EnumActivityPriority Priority,
    DateTimeOffset? DueDate) : IRequest<long>;