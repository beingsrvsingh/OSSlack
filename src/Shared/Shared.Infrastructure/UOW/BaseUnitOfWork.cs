using Microsoft.EntityFrameworkCore;
using System.Data;
using Shared.Domain.Entities;
using Shared.Domain.UOW;
using Shared.Domain.Entities.Interface;
using Mapster;
using Microsoft.EntityFrameworkCore.Storage;

namespace Shared.Infrastructur.UoW;

public abstract class BaseUnitOfWork<TContext, TAuditLog> : IBaseUnitOfWork, IDisposable
    where TContext : DbContext
    where TAuditLog : class
{
    protected readonly TContext _context;
    private IDbContextTransaction? _transaction;
    private IDbConnection? _connection;

    public IDbConnection Connection => _connection ??= _context.Database.GetDbConnection();
    public IDbContextTransaction? Transaction => _transaction;

    protected BaseUnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    
    public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
            throw new InvalidOperationException("Transaction already started.");

        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        return _transaction;
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction == null)
            throw new InvalidOperationException("Transaction not started.");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public virtual Task<int> SaveChangesAsync(bool trackChanges = false, string createdBy = "", DateTime? createdOn = null, int recordId = 0)
    {
        List<AuditEntry> auditEntries = new();
        if (trackChanges)
        {
            auditEntries = OnBeforeSaveChanges(recordId, createdBy, createdOn);
        }

        int result = _context.SaveChanges();

        if (trackChanges)
        {
            result += OnAfterSaveChanges(auditEntries);
        }

        return Task.FromResult(result);
    }

    protected virtual List<AuditEntry> OnBeforeSaveChanges(int recordId, string createdBy, DateTime? createdOn)
    {
        _context.ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();

        foreach (var entry in _context.ChangeTracker.Entries())
        {
            if (entry.Entity is IAuditIgnore || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;

            var auditEntry = new AuditEntry(entry)
            {
                TableName = entry.Entity.GetType().Name,
                CreatedBy = createdBy,
                CreatedOn = createdOn ?? DateTime.UtcNow
            };

            auditEntries.Add(auditEntry);

            foreach (var prop in entry.Properties)
            {
                if (prop.IsTemporary)
                {
                    auditEntry.TemporaryProperties.Add(prop);
                    continue;
                }

                var propertyName = prop.Metadata.Name;
                if (propertyName is "CreatedOn" or "ModifiedOn" or "CreatedBy" or "ModifiedBy")
                    continue;

                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues = recordId == 0 ? prop.CurrentValue?.ToString() : recordId.ToString();
                    continue;
                }

                object? newValue = prop.CurrentValue;
                object? oldValue = prop.OriginalValue;

                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.Action = AuditAction.Insert;
                        auditEntry.NewValues[propertyName] = newValue!;
                        break;

                    case EntityState.Deleted:
                        auditEntry.Action = AuditAction.Delete;
                        auditEntry.OldValues[propertyName] = oldValue!;
                        break;

                    case EntityState.Modified:
                        if (prop.IsModified)
                        {
                            auditEntry.Action = AuditAction.Update;
                            auditEntry.OldValues[propertyName] = oldValue!;
                            auditEntry.NewValues[propertyName] = newValue!;
                        }
                        break;
                }
            }
        }

        foreach (var entry in auditEntries.Where(e => !e.HasTemporaryProperties))
        {
            if (entry.OldValues.Count > 0 || entry.NewValues.Count > 0)
            {
                _context.Add(ConvertAuditEntry(entry));
            }
        }

        return auditEntries.Where(e => e.HasTemporaryProperties).ToList();
    }

    protected virtual int OnAfterSaveChanges(List<AuditEntry> auditEntries)
    {
        if (auditEntries is null || auditEntries.Count == 0)
            return 0;

        foreach (var auditEntry in auditEntries)
        {
            foreach (var prop in auditEntry.TemporaryProperties)
            {
                if (prop.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues = prop.CurrentValue?.ToString() ?? string.Empty;
                }
                else
                {
                    auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue!;
                }
            }

            _context.Add(ConvertAuditEntry(auditEntry));
        }

        return _context.SaveChanges();
    }

    protected abstract TAuditLog ConvertAuditEntry(AuditEntry entry);

    public void Dispose()
    {
        _transaction?.Dispose();
        _connection?.Dispose();
        _context.Dispose();
    }
}
