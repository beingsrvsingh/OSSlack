using Catalog.Domain.Core.Repository;
using Catalog.Domain.Core.UOW;
using Catalog.Domain.Entities;
using Catalog.Infrastructure.Persistence.Context;
using Mapster;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace Catalog.Infrastructure.Repositories.UOW
{
    public class UnitOfWork : BaseUnitOfWork<CatalogDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(
            CatalogDbContext context
        ) : base(context)
        {
        }

        public ICategoryRepository CatalogRepository => throw new NotImplementedException();

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}