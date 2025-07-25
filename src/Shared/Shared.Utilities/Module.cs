
using Autofac;

namespace Shared.Utilities;

public class SharedUtilitiesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(SharedUtilitiesModule).Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}