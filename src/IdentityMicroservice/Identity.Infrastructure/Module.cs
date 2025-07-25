using Autofac;

namespace Identity.Infrastructure;

public class IdentityInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterAssemblyTypes(typeof(IdentityInfrastructureModule).Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();
        }
}
