using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Review.Application.Config;
using System.Reflection;

namespace Review.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

            services.AddFluentValidationAutoValidation(config => { config.DisableDataAnnotationsValidation = true; });

            services.RegisterMapsterConfiguration();

            return services;
        }
    }
}
