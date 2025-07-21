using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Core.UOW;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Infrastructur.UoW;

namespace SecretManagement.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<SecretManagementDbContext>, IUnitOfWork
    {
        public ISecretRepository SecretRepository { get; }
        private IApiKeyRepository? apiKeyRepository;

        public UnitOfWork(
            SecretManagementDbContext context,
            ISecretRepository secretRepository
        ) : base(context)
        {
            SecretRepository = secretRepository;
        }

        public IApiKeyRepository ApiKeyRepository()
        {
            if (apiKeyRepository == null)
            {
                apiKeyRepository = new ApiKeyRepository(_context);
            }
            return apiKeyRepository;
        }
    }
}
