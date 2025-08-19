namespace Agenda.Application.ViewModels.IO.Activity;

public record OutputAcitivity(
    long Id,
    string Title,
    string? Description,
    DateTime DueDate,
    string Status
);