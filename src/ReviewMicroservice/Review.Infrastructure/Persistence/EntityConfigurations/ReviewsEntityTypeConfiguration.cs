using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewsEntityTypeConfiguration : IEntityTypeConfiguration<Review.Domain.Entities.Reviews>
    {
        public void Configure(EntityTypeBuilder<Review.Domain.Entities.Reviews> entity)
        {
            entity.HasKey(e => e.Id).HasName("PK_dbo.Reviews");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.ReviewReason).HasMaxLength(100);
            entity.Property(e => e.ReviewedBy).HasMaxLength(50);
            entity.Property(e => e.ReviewedDate).HasColumnType("datetime");
            entity.Property(e => e.ShortDescription).HasMaxLength(200);
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.UserId).HasMaxLength(50);
        }
    }
}
