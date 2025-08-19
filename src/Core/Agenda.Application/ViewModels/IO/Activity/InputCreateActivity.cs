using System.ComponentModel.DataAnnotations;

namespace Agenda.Application.ViewModels.IO.Activity;

public record InputCreateActivity(
    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "O título deve ter entre 1 e 100 caracteres.")]
    string Title,
    [MaxLength(500, ErrorMessage = "A descrição deve ter no maximo 500 caracteres.")]
    string? Description,
    [Required(ErrorMessage = "A data de finalização é obrigatória.")]
    DateTime DueDate
);