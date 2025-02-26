﻿using FluentValidation;
using GameService.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace GameService.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddValidatorsFromAssemblies(new[] { assembly }, includeInternalTypes: true);
        }
    }
}
