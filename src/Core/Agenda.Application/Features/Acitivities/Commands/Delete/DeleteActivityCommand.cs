using MediatR;

namespace Agenda.Application.Features.Activities.Commands.Delete;

public record DeleteActivityCommand(long Id) : IRequest<Unit>;