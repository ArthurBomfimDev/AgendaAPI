using Agenda.Domain.Enuns.ActivityPriority;
using System.ComponentModel.DataAnnotations;

namespace Agenda.Application.ViewModels.DTO.Activity;

public record InputCreateActivity(
    [Required(ErrorMessage = "O título é obrigatório.")]
    [MaxLength(100, ErrorMessage = "O título deve ter entre 1 e 100 caracteres.")]
    string Title,
    [MaxLength(500, ErrorMessage = "A descrição deve ter no maximo 500 caracteres.")]
    string? Description,
    [Required(ErrorMessage = "A prioridade da tarefa é um campo obrigatorio. 1 -> Baixa Prioridade | 2 -> Média Prioridade | 3 -> Alta Prioridade ")]
    EnumActivityPriority Priority,
    DateTime? DueDate
);