using Autofac;
using Shared.Contracts.Interfaces;
using Shared.Infrastructure.Platform;

namespace Shared.Infrastructure
{
    public class SharedInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SharedInfrastructureModule).Assembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();

            builder.Register(ctx => PlatformServiceFactory.Create())
            .As<IPlatformService>()
            .SingleInstance();
        }
    }
}
