using Autofac;
using SecretManagement.Application.Services.Interfaces;
using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Core.UOW;
using SecretManagement.Infrastructure.Repositories;
using SecretManagement.Infrastructure.Services.Secrets;

namespace SecretManagement.Infrastructure;

public class SecretManagementInfrastructureModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
        builder.RegisterType<SecretRepository>().As<ISecretRepository>().InstancePerLifetimeScope();
        builder.RegisterType<SecretsService>().As<ISecretsService>().InstancePerLifetimeScope();
    }
}
