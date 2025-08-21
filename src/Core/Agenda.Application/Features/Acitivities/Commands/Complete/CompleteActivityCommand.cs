using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Complete;

public record CompleteActivityCommand(long Id) : IRequest<Unit>;