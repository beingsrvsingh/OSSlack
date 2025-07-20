using Autofac;

namespace Shared.Infrastructure
{
    public class SharedInfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(SharedInfrastructureModule).Assembly)
                   .AsImplementedInterfaces()
                   .InstancePerLifetimeScope();
        }
    }
}
