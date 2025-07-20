using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Core.UOW;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Infrastructur.UoW;

namespace SecretManagement.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<SecretManagementDbContext>, IUnitOfWork
    {
        public ISecretRepository SecretRepository { get; }

        public UnitOfWork(
            SecretManagementDbContext context,
            ISecretRepository secretRepository
        ) : base(context)
        {
            SecretRepository = secretRepository;
        }
    }
}
