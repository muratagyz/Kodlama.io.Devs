using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Application.Features.Auth.Rules;
using Application.Features.Github.Rules;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technology.Rules;
using Core.Application.Pipelines.Authorization;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped<ProgrammingLanguageBusinessRules>();
        services.AddScoped<TechnologyBusinessRules>();
        services.AddScoped<AuthBusinessRules>();
        services.AddScoped<GithubBusinessRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        return services;
    }
}