using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Review.Infrastructure.Persistence.EntityConfigurations
{
       public class ReviewReportConfiguration : IEntityTypeConfiguration<ReviewReport>
       {
              public void Configure(EntityTypeBuilder<ReviewReport> builder)
              {
                     builder.ToTable("review_reports");

                     builder.HasKey(r => r.Id);

                     builder.Property(r => r.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(r => r.ReviewId)
                            .HasColumnName("review_id")
                            .IsRequired();

                     builder.Property(r => r.ReportedByUserId)
                            .HasColumnName("reported_by_user_id")
                            .IsRequired()
                            .HasMaxLength(100);

                     builder.Property(r => r.Reason)
                            .HasColumnName("reason")
                            .IsRequired()
                            .HasMaxLength(255);

                     builder.Property(r => r.ReportedAt)
                            .HasColumnName("reported_at")
                            .IsRequired();

                     builder.Property(r => r.Status)
                            .HasColumnName("status")
                            .HasConversion<int>()
                            .IsRequired();

                     builder.Property(r => r.ResolutionNote)
                            .HasColumnName("resolution_note")
                            .HasMaxLength(1000);

                     builder.Property(r => r.ResolvedAt)
                            .HasColumnName("resolved_at");

                     builder.Property(r => r.ResolvedByUserId)
                            .HasColumnName("resolved_by_user_id")
                            .HasMaxLength(100);

                     // Add foreign key and cascade delete on Review deletion
                     builder.HasOne(rm => rm.Review)
                            .WithMany(r => r.Reports)
                            .HasForeignKey(r => r.ReviewId)
                            .OnDelete(DeleteBehavior.Cascade)
                            .HasConstraintName("fk_review_reports_review_id");
              }

       }
}