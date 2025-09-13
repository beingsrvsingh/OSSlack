using Microsoft.EntityFrameworkCore;
using SearchAggregator.Domain.Entities;
using System.Reflection;


namespace SearchAggregator.Infrastructure.Persistence.Context;

public partial class SearchDbContext(DbContextOptions<SearchDbContext> options) : DbContext(options)
{
    public virtual DbSet<UserSearchHistory> UserSearchHistories => Set<UserSearchHistory>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
