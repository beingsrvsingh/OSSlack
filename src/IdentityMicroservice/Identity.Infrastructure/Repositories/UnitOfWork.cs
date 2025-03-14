using Identity.Domain.Core.Repository;
using Identity.Domain.Core.UOW;
using Identity.Domain.Entities;
using Identity.Infrastructure.Persistence.Context;
using Mapster;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using System.Data;

namespace Identity.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        protected readonly ApplicationDbContext _context;
        private bool _disposed = false;
        private IDbConnection _connection;
        private IDbTransaction? _transaction;

        public IDbConnection Connection => this._connection;
        public IDbTransaction Transaction => this._transaction;

        private IApplicationUserRepository? applicationUserRepository;
        private IUserInfoRepository? userInfoRepository;
        private IRefreshTokenRepository? refreshTokenRepository;
        private IAddressRepository? addressRepository;
        private IUserDevicesRepository? userDevicesRepository;
        private ICountryMasterRepository? countryMasterRepository;
        private IStateMasterRepository? stateMasterRepository;
        private ICityMasterRepository? areaMasterRepository;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public ICountryMasterRepository CountryMasterRepository
        {
            get
            {
                if (countryMasterRepository == null)
                {
                    countryMasterRepository = new CountryMasterRepository(_context);
                }
                return countryMasterRepository;
            }
        }

        public IStateMasterRepository StateMasterRepository
        {
            get
            {
                if (stateMasterRepository == null)
                {
                    stateMasterRepository = new StateMasterRepository(_context);
                }
                return stateMasterRepository;
            }
        }

        public ICityMasterRepository CityMasterRepository
        {
            get
            {
                if (areaMasterRepository == null)
                {
                    areaMasterRepository = new CityMasterRepository(_context);
                }
                return areaMasterRepository;
            }
        }

        public IUserInfoRepository UserInfoRepository
        {
            get
            {
                if (userInfoRepository == null)
                {
                    userInfoRepository = new UserInfoRepository(_context);
                }
                return userInfoRepository;
            }
        }

        public IApplicationUserRepository ApplicationUserRepository
        {
            get
            {
                if (applicationUserRepository == null)
                {
                    applicationUserRepository = new ApplicationUserRepository(_context);
                }
                return applicationUserRepository;
            }
        }

        public IUserDevicesRepository UserDevicesRepository
        {
            get
            {
                if (userDevicesRepository == null)
                {
                    userDevicesRepository = new UserDevicesRepository(_context);
                }
                return userDevicesRepository;
            }
        }

        public IRefreshTokenRepository RefreshTokenRepository
        {
            get
            {
                if (refreshTokenRepository == null)
                {
                    refreshTokenRepository = new RefreshTokenRepository(_context);
                }
                return refreshTokenRepository;
            }
        }

        public IAddressRepository AddressRepository
        {
            get
            {
                if (addressRepository is null)
                {
                    addressRepository = new AddressRepository(_context);
                }
                return addressRepository;
            }
        }        

        public Task<int> SaveChangesAsync(bool trackChanges = false, string createdBy = "", DateTime? createdOn = null, int recordId = 0)
        {
            List<AuditEntry> auditEntries = new();
            if (trackChanges)
            {
                auditEntries = OnBeforeSaveChanges(recordId, createdBy, createdOn);
            }

            int result = 0;
            result = _context.SaveChanges();

            if (trackChanges)
            {
                result = OnAfterSaveChanges(auditEntries);
            }

            return Task.FromResult(result);
        }

        private List<AuditEntry> OnBeforeSaveChanges(int recordId, string createdBy, DateTime? createdOn)
        {
            this._context.ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in this._context.ChangeTracker.Entries())
            {
                if (entry.Entity is AspNetUserAuditLog || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name; // entry.Metadata.Relational().TableName;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;

                    if (propertyName == "CreatedOn" || propertyName == "ModifiedOn" || propertyName == "CreatedBy" || propertyName == "ModifiedBy")
                    {
                        continue;
                    }

                    if (property.Metadata.IsPrimaryKey())
                    {
                        if (recordId == 0)
                        {
                            auditEntry.KeyValues = property.CurrentValue?.ToString()!;
                            continue;
                        }
                        else
                        {
                            auditEntry.KeyValues = recordId.ToString();
                            continue;
                        }
                    }

                    auditEntry.CreatedOn = createdOn.HasValue ? createdOn.Value : DateTime.Now;
                    auditEntry.CreatedBy = createdBy;
                    object? newPropertyValues = property.CurrentValue;
                    object? oldPropertyValues = property.OriginalValue;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.Action = EntityState.Added.ToString();
                            auditEntry.NewValues[propertyName] = newPropertyValues!;
                            break;

                        case EntityState.Deleted:
                            auditEntry.Action = EntityState.Deleted.ToString();
                            auditEntry.OldValues[propertyName] = oldPropertyValues!;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.Action = EntityState.Modified.ToString();
                                auditEntry.OldValues[propertyName] = oldPropertyValues!;
                                auditEntry.NewValues[propertyName] = newPropertyValues!;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                if (auditEntry.OldValues.Count > 0 || auditEntry.NewValues.Count > 0)
                {
                    var entry = auditEntry.ToAudit().Adapt<AspNetUserAuditLog>();
                    this._context.AspNetUserAuditLogs.Add(entry);
                }
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private int OnAfterSaveChanges(List<AuditEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return 0;

            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues = prop.CurrentValue!.ToString()!;
                    }
                    else
                    {
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue!;
                    }
                }

                // Save the Audit entry
                var entry = auditEntry.ToAudit().Adapt<AspNetUserAuditLog>();
                this._context.AspNetUserAuditLogs.Add(entry);
            }

            return this._context.SaveChanges();
        }

        public void Begin()
        {
            this._transaction = this.Connection?.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                this._transaction!.Commit();
            }
            catch
            {
                this._transaction!.Rollback();
                throw;
            }
            finally
            {
                this._transaction!.Dispose();
                this._transaction = this.Connection?.BeginTransaction();
            }
        }

        public void Rollback()
        {
            try
            {
                this._transaction!.Rollback();
            }
            catch
            {
                this._transaction!.Dispose();
                this._transaction = this.Connection?.BeginTransaction();
            }
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // Dispose managed state (managed objects).
                Connection?.Dispose();
                _transaction?.Dispose();
            }

            _disposed = true;
        }

        ~UnitOfWork()
        {
            Dispose(true);
        }
    }
}