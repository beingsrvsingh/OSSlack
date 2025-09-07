using Kathavachak.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Kathavachak.Infrastructure.Persistence.EntityConfigurations
{
    public class KathavachakTopicConfiguration : IEntityTypeConfiguration<KathavachakTopic>
    {
        public void Configure(EntityTypeBuilder<KathavachakTopic> builder)
        {
            builder.ToTable("kathavachak_topic");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .HasColumnName("id");

            builder.Property(t => t.KathavachakId)
                .HasColumnName("kathavachak_id")
                .IsRequired();

            builder.Property(t => t.Title)
                .HasColumnName("topic_name")
                .HasMaxLength(200)
                .IsRequired();

            builder.HasOne(t => t.Kathavachak)
                .WithMany(k => k.Topics)
                .HasForeignKey(t => t.KathavachakId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
