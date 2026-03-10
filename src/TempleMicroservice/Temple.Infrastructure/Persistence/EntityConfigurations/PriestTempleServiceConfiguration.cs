using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

public class PriestTempleServiceConfiguration : IEntityTypeConfiguration<PriestTempleService>
{
    public void Configure(EntityTypeBuilder<PriestTempleService> entity)
    {
        // Table name
        entity.ToTable("priest_temple_service");

        // Primary key
        entity.HasKey(e => e.Id)
              .HasName("pk_priest_temple_service");

        // Columns (snake_case)
        entity.Property(e => e.Id).HasColumnName("id");
        entity.Property(e => e.PriestId).HasColumnName("priest_id").IsRequired();
        entity.Property(e => e.PriestExpertiseId).HasColumnName("priest_expertise_id").IsRequired();
        entity.Property(e => e.PriestExpertiseModeId).HasColumnName("priest_expertise_mode_id");
        entity.Property(e => e.TempleId).HasColumnName("temple_id").IsRequired();

        entity.Property(e => e.PriestNameSnapshot)
              .HasColumnName("priest_name_snapshot")
              .HasMaxLength(150);

        entity.Property(e => e.ExpertiseNameSnapshot)
              .HasColumnName("expertise_name_snapshot")
              .HasMaxLength(150);

        entity.Property(e => e.ModeNameSnapshot)
              .HasColumnName("mode_name_snapshot")
              .HasMaxLength(50);

        entity.Property(e => e.IsActive)
              .HasColumnName("is_active")
              .HasDefaultValue(true);

        entity.Property(e => e.StartDate).HasColumnName("start_date");
        entity.Property(e => e.EndDate).HasColumnName("end_date");

        entity.Property(e => e.Notes)
              .HasColumnName("notes")
              .HasMaxLength(500);

        entity.Property(e => e.CreatedAt)
              .HasColumnName("created_at")
              .HasDefaultValueSql("CURRENT_TIMESTAMP");

        entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");

        // Relationships (optional, but can be mapped to IDs only)
        entity.HasOne(e => e.Mode)
              .WithMany()
              .HasForeignKey(e => e.PriestExpertiseModeId)
              .HasConstraintName("fk_priest_temple_service_mode")
              .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        entity.HasIndex(e => new { e.TempleId, e.PriestId, e.PriestExpertiseId })
              .HasDatabaseName("idx_temple_priest_expertise")
              .IsUnique();
    }
}