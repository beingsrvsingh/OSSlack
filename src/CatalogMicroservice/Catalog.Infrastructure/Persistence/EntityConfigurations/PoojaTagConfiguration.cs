using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaTagConfiguration : IEntityTypeConfiguration<PoojaTag>
    {
        public void Configure(EntityTypeBuilder<PoojaTag> builder)
        {
            builder.ToTable("pooja_tag");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.TagName)
                .IsRequired()
                .HasMaxLength(50);
        }
    }

}