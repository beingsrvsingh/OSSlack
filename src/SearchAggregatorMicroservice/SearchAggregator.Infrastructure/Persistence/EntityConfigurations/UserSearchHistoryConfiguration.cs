using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SearchAggregator.Domain.Entities
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserSearchHistoryConfig : IEntityTypeConfiguration<UserSearchHistory>
    {
        public void Configure(EntityTypeBuilder<UserSearchHistory> builder)
        {
            builder.ToTable("user_search_history"); // Table name in snake_case

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id)
                   .HasColumnName("id")
                   .ValueGeneratedOnAdd();

            builder.Property(e => e.UserId)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnName("user_id");

            builder.Property(e => e.Query)
                   .IsRequired()
                   .HasMaxLength(500)
                   .HasColumnName("query");

            builder.Property(e => e.Platform)
                   .HasMaxLength(50)
                   .HasColumnName("platform");

            builder.Property(e => e.Language)
                   .HasMaxLength(20)
                   .HasColumnName("language");

            builder.Property(e => e.IPAddress)
                   .HasMaxLength(45) // IPv6 max length
                   .HasColumnName("ip_address");

            builder.Property(e => e.ResultCount)
                   .HasColumnName("result_count");

            builder.Property(e => e.SearchedAt)
                   .HasColumnName("searched_at")
                   .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        }
    }

}
