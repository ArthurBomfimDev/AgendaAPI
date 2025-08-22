using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Cancel;

public record CancelActivityCommand(long Id) : IRequest<Unit>;