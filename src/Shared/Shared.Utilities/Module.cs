
using Autofac;

namespace Shared.Utilities;

public class UtilitiesModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterAssemblyTypes(typeof(UtilitiesModule).Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
    }
}