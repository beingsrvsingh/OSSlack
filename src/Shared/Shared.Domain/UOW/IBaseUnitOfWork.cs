using System.Data;
using Shared.Domain.Common.Entities.Interface;

namespace Shared.Domain.UOW
{
    public interface IBaseUnitOfWork : IAuditLog, IDisposable
    {
        IDbConnection? Connection { get; }
        IDbTransaction? Transaction { get; }
        void BeginTransaction();
        void CommitTransaction();
        void RollbackTransaction();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        string? GetSQL(string resourcePath)
        {
            if (string.IsNullOrEmpty(resourcePath)) return "Resource path either null or empty.";
            using StreamReader sr = new(resourcePath);
            if (sr is null)
                return "Resource path is not valid.";
            return sr.ReadToEnd();
        }
    }
}
