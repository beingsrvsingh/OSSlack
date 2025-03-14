using Review.Domain.Repository;
using Shared.Domain.Common.Entities.Interface;
using Shared.Domain.UOW;

namespace Review.Domain.Core.UOW
{
    public interface IUnitOfWork : IBaseUnitOfWork, IAuditLog, IDisposable
    {
        IReviewRepository ReviewRepository { get; }
        IReviewDetailRepository ReviewReportDetailRepository { get; }
        IReportLookUpRepository ReviewReportLookUpRepository { get; }

    }
}
