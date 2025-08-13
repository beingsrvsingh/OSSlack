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
        private readonly CatalogDbContext _catalogDbContext;
        public UnitOfWork(
            CatalogDbContext context
        ) : base(context)
        {
            this._catalogDbContext = context;
        }

        private ICategoryRepository? categoryRepository;
        private ISubCategoryRepository? subCategoryRepository;
        private IPoojaKitItemRepository? poojaKitItemRepository;

        public ICategoryRepository CatalogRepository => categoryRepository ?? new CategoryRepository(_catalogDbContext);

        public ISubCategoryRepository SubCategoryRepository => subCategoryRepository ?? new SubCategoryRepository(_catalogDbContext);

        public IPoojaKitItemRepository PoojaKitItemRepository => poojaKitItemRepository ?? new PoojaKitItemRepository(_catalogDbContext);

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}