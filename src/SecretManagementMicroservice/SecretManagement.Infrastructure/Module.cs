using Autofac;

namespace SecretManagement.Infrastructure;

public class SecretManagementInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {

        builder.RegisterAssemblyTypes(typeof(SecretManagementInfrastructureModule).Assembly)
               .AsImplementedInterfaces()
               .InstancePerLifetimeScope();

    }
}
