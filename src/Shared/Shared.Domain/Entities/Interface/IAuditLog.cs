namespace Shared.Domain.Common.Entities.Interface
{
    public interface IAuditLog
    {
        Task<int> SaveChangesAsync(bool trackChanges = false, string createdBy = "", DateTime? createdOn = null, int recordId = 0);
    }
}
