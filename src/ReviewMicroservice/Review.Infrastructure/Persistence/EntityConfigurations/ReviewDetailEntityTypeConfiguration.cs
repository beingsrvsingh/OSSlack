using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewDetailEntityTypeConfiguration : IEntityTypeConfiguration<ReviewDetail>
    {
        public void Configure(EntityTypeBuilder<ReviewDetail> entity)
        {            
            entity.HasKey(e => e.Id).HasName("PK_ReviewDetails");

            entity.HasIndex(e => e.ReviewReportLookUpId, "IX_ReviewDetail_ReportTypeId");

            entity.HasIndex(e => e.ReviewId, "IX_ReviewDetail_ReviewId");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.IsHelpful).HasColumnName("isHelpful");
            entity.Property(e => e.Message).HasMaxLength(200);
            entity.Property(e => e.UserId).HasMaxLength(128);

            entity.HasOne(d => d.Review).WithMany(p => p.ReviewDetails)
                .HasForeignKey(d => d.ReviewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewDetail_Reviews");

            entity.HasOne(d => d.ReviewReportLookUp).WithMany(p => p.ReviewDetails)
                .HasForeignKey(d => d.ReviewReportLookUpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReviewDetail_ReviewReportLookup");
        }
    }
}
