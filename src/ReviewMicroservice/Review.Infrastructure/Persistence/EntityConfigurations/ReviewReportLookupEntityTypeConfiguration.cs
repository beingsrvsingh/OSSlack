using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewReportLookupEntityTypeConfiguration : IEntityTypeConfiguration<ReviewReportLookup>
    {
        public void Configure(EntityTypeBuilder<ReviewReportLookup> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_ReviewReportLookups");

            entity.ToTable("ReviewReportLookup");

            entity.Property(e => e.Descriptions).HasMaxLength(100);
            entity.Property(e => e.DisplayName).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(50);
        }
    }
}
