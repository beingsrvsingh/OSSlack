using Product.Domain.Core.UOW;
using Mapster;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;
using Product.Infrastructure.Persistence.Context;
using Product.Domain.Repository;
using Product.Domain.Entities;

namespace Product.Infrastructure.Repositories.UOW
{
    public class UnitOfWork : BaseUnitOfWork<ProductDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(
            ProductDbContext context
        ) : base(context)
        {
        }

        public IProductRepository ProductRepository => throw new NotImplementedException();

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}