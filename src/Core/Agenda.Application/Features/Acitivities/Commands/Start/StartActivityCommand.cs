using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Start;

public record StartActivityCommand(long Id) : IRequest<Unit>;