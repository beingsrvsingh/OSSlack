using Product.Domain.Core.UOW;
using Mapster;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;
using Product.Infrastructure.Persistence.Context;
using Product.Domain.Repository;
using Product.Domain.Entities;
using Product.Domain.Core.Repository;

namespace Product.Infrastructure.Repositories.UOW
{
    public class UnitOfWork : BaseUnitOfWork<ProductDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(
            ProductDbContext context
        ) : base(context)
        {
        }

        private IProductRepository? productRepository;
        private IProductAttributeRepository? productAttributeRepository;
        public IProductRepository ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(_context);
                }
                return productRepository;
            }
        }         

        public IProductAttributeRepository ProductAttributeRepository
        {
            get
            {
                if (productAttributeRepository == null)
                {
                    productAttributeRepository = new ProductAttributeRepository(_context);
                }
                return productAttributeRepository;
            }
        }

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}