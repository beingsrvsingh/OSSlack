using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Temple.Domain.Entities;

public class PriestTempleConfiguration : IEntityTypeConfiguration<PriestTemple>
{
    public void Configure(EntityTypeBuilder<PriestTemple> entity)
    {
        entity.ToTable("priest_temple"); // snake_case table name

        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        entity.Property(e => e.PriestId)
            .HasColumnName("priest_id");

        entity.Property(e => e.TempleId)
            .HasColumnName("temple_id");

        entity.Property(e => e.AssignedAt)
            .HasColumnName("assigned_at")
            .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

        entity.Property(e => e.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        entity.Property(e => e.PriestNameSnapshot)
            .HasColumnName("priest_name_snapshot")
            .HasMaxLength(150);

        entity.HasOne<TempleMaster>()
            .WithMany()
            .HasForeignKey(e => e.TempleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}