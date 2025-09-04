using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogUI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CatalogUI.Infrastructure.Persistence.Context
{
    public class CatalogUIDbContext : DbContext
    {
        public CatalogUIDbContext(DbContextOptions<CatalogUIDbContext> options)
            : base(options)
        { }

        public DbSet<Layout> Layouts => Set<Layout>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}