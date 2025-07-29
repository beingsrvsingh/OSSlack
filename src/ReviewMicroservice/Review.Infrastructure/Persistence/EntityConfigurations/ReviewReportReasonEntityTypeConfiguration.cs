using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Identity.Infrastructure.Persistence.EntityConfigurations
{
       public class ReviewReportReasonConfiguration : IEntityTypeConfiguration<ReviewReportReason>
       {
              public void Configure(EntityTypeBuilder<ReviewReportReason> builder)
              {
                     builder.ToTable("review_report_reasons");

                     builder.HasKey(r => r.Id);

                     builder.Property(r => r.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(r => r.Title)
                            .HasColumnName("title")
                            .IsRequired()
                            .HasMaxLength(200); 

                     builder.Property(r => r.DisplayName)
                            .HasColumnName("display_name")
                            .IsRequired()
                            .HasMaxLength(200);

                     builder.Property(r => r.Descriptions)
                            .HasColumnName("description")
                            .IsRequired(false)
                            .HasMaxLength(1000);

                     builder.Property(r => r.DisplayOrder)
                            .HasColumnName("display_order")
                            .IsRequired();
              }

       }
}
