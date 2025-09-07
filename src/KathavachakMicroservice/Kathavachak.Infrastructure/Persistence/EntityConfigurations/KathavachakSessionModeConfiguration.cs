using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakSessionModeConfiguration : IEntityTypeConfiguration<KathavachakSessionMode>
    {
        public void Configure(EntityTypeBuilder<KathavachakSessionMode> builder)
        {
            builder.ToTable("kathavachak_session_mode");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .HasColumnName("id");

            builder.Property(s => s.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(s => s.Mode)
                .HasColumnName("mode_name")
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(s => s.Kathavachak)
                .WithMany(k => k.SessionModes)
                .HasForeignKey(s => s.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
