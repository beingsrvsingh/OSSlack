using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class ChildSubCategoryMasterEntityTypeConfiguration : IEntityTypeConfiguration<ChildSubCategoryMaster>
    {
        public void Configure(EntityTypeBuilder<ChildSubCategoryMaster> entity)
        {
            entity.ToTable("ChildSubCategoryMaster");

            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.SubCategory).WithMany(p => p.ChildSubCategoryMasters)
                .HasForeignKey(d => d.SubCategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ChildSubCategoryMaster_SubCategoryMaster");
        }
    }
}
