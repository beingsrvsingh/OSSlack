using Mapster;
using SecretManagement.Domain.Core.Repository;
using SecretManagement.Domain.Core.UOW;
using SecretManagement.Domain.Entities;
using SecretManagement.Infrastructure.Persistence.Context;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace SecretManagement.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<SecretManagementDbContext, AuditLog>, IUnitOfWork
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

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}
