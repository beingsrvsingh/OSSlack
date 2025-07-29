using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Review.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewFeedbackConfiguration : IEntityTypeConfiguration<ReviewFeedback>
    {
        public void Configure(EntityTypeBuilder<ReviewFeedback> builder)
        {
            builder.ToTable("review_feedbacks");

            builder.HasKey(rf => rf.Id);

            builder.Property(rf => rf.Id)
                   .HasColumnName("id");

            builder.Property(rf => rf.ReviewId)
                   .HasColumnName("review_id");

            builder.Property(rf => rf.UserId)
                   .IsRequired()
                   .HasColumnName("user_id");

            builder.Property(rf => rf.IsHelpful)
                   .IsRequired()
                   .HasColumnName("is_helpful");

            builder.Property(rf => rf.CreatedAt)
                   .IsRequired()
                   .HasColumnName("created_at");

            builder.HasOne(rf => rf.Review)
                   .WithMany(r => r.Feedbacks)
                   .HasForeignKey(rf => rf.ReviewId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(rf => new { rf.ReviewId, rf.UserId })
                   .IsUnique()
                   .HasDatabaseName("ix_review_feedbacks_review_id_user_id");
        }

    }
}