using Agenda.Application.Common.Behaviour;
using Agenda.Application.Features.Activities.Commands.Create;
using Agenda.Application.Interfaces.Context;
using Agenda.Application.Interfaces.Repository.Read;
using Agenda.Application.Interfaces.Repository.Write;
using Agenda.Application.Interfaces.UnitOfWork;
using Agenda.Infrastructure.Persistence;
using Agenda.Infrastructure.Repositories.Read;
using Agenda.Infrastructure.Repositories.Write;
using Agenda.Infrastructure.UnitOfWork;
using FluentValidation;
using MediatR;

namespace Agenda.Presentation.Extensions;

public static class InjectionDependencyExtension
{
    public static IServiceCollection ConfigureInjectionDependency(this IServiceCollection services)
    {
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<IActivityReadRepository, ActivityReadRepository>();
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<AppDbContext>());
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        var applicationAssembly = typeof(CreateActivityCommand).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
        services.AddValidatorsFromAssembly(applicationAssembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        return services;
    }
}