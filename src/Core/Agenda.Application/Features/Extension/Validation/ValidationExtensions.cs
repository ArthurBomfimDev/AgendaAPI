using FluentValidation;

namespace Agenda.Application.Features.Extension.Validation;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, long> ValidateId<T>(this IRuleBuilder<T, long> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty().WithMessage("O Id é obrigatorio")
            .GreaterThan(0).WithMessage("O Id dever ser positivo maior do que 0");
    }
}