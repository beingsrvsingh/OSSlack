using Autofac;
using Microsoft.Extensions.Configuration;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Contracts.Interfaces;
using Shared.Infrastructure.Http;
using Shared.Infrastructure.Platform;
using Shared.Infrastructure.Services;

namespace Shared.Infrastructure
{
    public class SharedInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SharedInfrastructureModule).Assembly)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(LoggerService<>))
                .As(typeof(ILoggerService<>))
                .InstancePerLifetimeScope();

            builder.RegisterType<RemoteSecretManager>().SingleInstance();
            builder.RegisterType<WindowsSecretManager>().SingleInstance();
            builder.RegisterType<MacKeychainManager>().SingleInstance();
            builder.RegisterType<LinuxSecretManager>().SingleInstance();

        }
    }
}
