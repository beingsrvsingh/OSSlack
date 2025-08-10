using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Temple.Infrastructure.Persistence.EntityConfigurations
{
       public class TempleEntityConfiguration : IEntityTypeConfiguration<TempleMaster>
       {
              public void Configure(EntityTypeBuilder<TempleMaster> builder)
              {
                     builder.ToTable("temples");

                     builder.HasKey(a => a.Id);

                     builder.Property(a => a.UserId)
                            .IsRequired()
                            .HasMaxLength(36);

                     builder.Property(a => a.DisplayName)
                            .HasMaxLength(200);

                     builder.Property(a => a.ProfilePictureUrl)
                            .HasMaxLength(500);

                     builder.Property(a => a.AverageRating)
                            .HasColumnType("decimal(3,2)")
                            .HasDefaultValue(0m);

                     builder.Property(a => a.TotalRatings)
                            .HasDefaultValue(0);

                     builder.Property(a => a.IsActive)
                            .HasDefaultValue(true);

                     builder.Property(a => a.CreatedAt)
                            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

                     builder.Property(a => a.UpdatedAt)
                            .IsRequired(false);

                     builder.Property(a => a.ConsultationModes)
                            .HasDefaultValue(ConsultationMode.None);

                     builder.HasMany(a => a.AstrologerLanguages)
                            .WithOne(al => al.TempleMaster)
                            .HasForeignKey(al => al.AstrologerId)
                            .HasConstraintName("fk_astrologer_language_astrologer_id")
                            .OnDelete(DeleteBehavior.Cascade);

                     builder.HasMany(a => a.TempleExpertises)
                            .WithOne(ae => ae.TempleMaster)
                            .HasForeignKey(ae => ae.AstrologerId)
                            .HasConstraintName("fk_astrologer_expertise_astrologer_id")
                            .OnDelete(DeleteBehavior.Cascade);
              }
       }
}