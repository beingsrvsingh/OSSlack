
using Autofac;

namespace Shared.Infrastructure;

public class SharedUtilitiesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(SharedUtilitiesModule).Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}