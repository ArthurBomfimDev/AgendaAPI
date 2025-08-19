using Agenda.Domain.Enuns.ActivityStatus;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Application.ViewModels.IO.Activity;

public record InputUpdateActivityStatus(
    [Required(ErrorMessage = "O status é obrigatório.")]
    EnumActivityStatus Status
);