using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Review.Domain.Entities;

namespace Review.Infrastructure.Persistence.EntityConfigurations
{
       public class ReviewMediaConfiguration : IEntityTypeConfiguration<ReviewMedia>
       {
              public void Configure(EntityTypeBuilder<ReviewMedia> builder)
              {
                     builder.ToTable("review_media");

                     builder.HasKey(rm => rm.Id);

                     builder.Property(rm => rm.Id)
                            .HasColumnName("id")
                            .ValueGeneratedOnAdd();

                     builder.Property(rm => rm.ReviewId)
                            .HasColumnName("review_id")
                            .IsRequired();

                     builder.Property(rm => rm.Url)
                            .HasColumnName("url")
                            .HasMaxLength(1000)
                            .IsRequired();

                     builder.Property(rm => rm.Type)
                            .HasColumnName("type")
                            .HasConversion<string>()
                            .IsRequired();

                     builder.HasOne(rm => rm.Review)
                            .WithMany(r => r.Medias)
                            .HasForeignKey(rm => rm.ReviewId)
                            .HasConstraintName("fk_review_media_review_id")
                            .OnDelete(DeleteBehavior.Cascade);
              }

       }
}