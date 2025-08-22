using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Pause;

public record PauseActivityCommand(long Id) : IRequest<Unit>;