using System.Data;

namespace Shared.Domain.UOW
{
    public interface IBaseUnitOfWork
    {
        IDbConnection Connection { get; }
        IDbTransaction Transaction { get; }
        void Begin();
        void Commit();
        void Rollback();
        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

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
