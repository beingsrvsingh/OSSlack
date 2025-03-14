using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class SubCategoryMasterEntityTypeConfiguration : IEntityTypeConfiguration<SubCategoryMaster>
    {
        public void Configure(EntityTypeBuilder<SubCategoryMaster> entity)
        {
            entity.ToTable("SubCategoryMaster");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Category).WithMany(p => p.SubCategoryMasters)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_SubCategoryMaster_CategoryMaster");
        }
    }
}
