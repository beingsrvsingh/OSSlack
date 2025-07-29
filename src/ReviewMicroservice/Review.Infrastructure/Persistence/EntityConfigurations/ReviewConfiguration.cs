using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Infrastructure.Persistence.EntityConfigurations
{
    public class ReviewConfiguration : IEntityTypeConfiguration<ReviewEntity>
    {
        public void Configure(EntityTypeBuilder<ReviewEntity> builder)
        {
            builder.ToTable("reviews");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasColumnName("id");

            builder.Property(r => r.ProductId)
                .IsRequired()
                .HasColumnName("product_id");

            builder.Property(r => r.UserId)
                .IsRequired()
                .HasMaxLength(450)
                .HasColumnName("user_id");

            builder.Property(r => r.Rating)
                .IsRequired()
                .HasColumnName("rating");

            builder.Property(r => r.Title)
                .HasMaxLength(200)
                .IsRequired(false)
                .HasColumnName("title");

            builder.Property(r => r.Comment)
                .HasMaxLength(1000)
                .IsRequired(false)
                .HasColumnName("comment");

            builder.Property(r => r.HelpfulCount)
                .HasDefaultValue(0)
                .HasColumnName("helpful_count");

            builder.Property(r => r.UnhelpfulCount)
                .HasDefaultValue(0)
                .HasColumnName("unhelpful_count");

            builder.Property(r => r.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder.Property(r => r.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("updated_at");

            builder.Property(r => r.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.Property(r => r.ModerationComment)
                .HasMaxLength(500)
                .IsRequired(false)
                .HasColumnName("moderation_comment");

            builder.Property(r => r.ModeratedByUserId)
                .HasMaxLength(450)
                .IsRequired(false)
                .HasColumnName("moderated_by_user_id");

            builder.Property(r => r.ModeratedAt)
                .IsRequired(false)
                .HasColumnName("moderated_at");

            // Navigation to related entities
            builder.HasMany(r => r.Feedbacks)
                .WithOne()
                .HasForeignKey(f => f.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Medias)
                .WithOne()
                .HasForeignKey(m => m.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(r => r.Reports)
                .WithOne()
                .HasForeignKey(rep => rep.ReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}