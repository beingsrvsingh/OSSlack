using Mapster;
using Review.Domain.Core.Repository;
using Review.Domain.Core.UOW;
using Review.Domain.Entities;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Review.Infrastructure.Repositories;
using Shared.Domain.Entities;
using Shared.Infrastructur.UoW;

namespace Shared.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<ReviewDbContext, AuditLog>, IUnitOfWork
    {
        public UnitOfWork(ReviewDbContext dbContext) : base(dbContext)
        { }

        private IReviewRepository? reviewRepository;
        private IReviewReportReasonRepository? reviewReportReasonRepository;
        private IReviewReportRepository? reviewReportRepository;
        public IReviewFeedbackRepository? reviewFeedbackRepository;
        public IReviewMediaRepository? reviewMediaRepository;

        public IReviewRepository ReviewRepository
        {
            get
            {
                if (reviewRepository == null)
                {
                    reviewRepository = new ReviewRepository(_context);
                }
                return reviewRepository;
            }
        }

        public IReviewReportRepository ReviewReportRepository
        {
            get
            {
                if (reviewReportRepository == null)
                {
                    reviewReportRepository = new ReviewReportRepository(_context);
                }
                return reviewReportRepository;
            }
        }

        public IReviewReportReasonRepository ReviewReportReasonRepository
        {
            get
            {
                if (reviewReportReasonRepository == null)
                {
                    reviewReportReasonRepository = new ReviewReportReasonRepository(_context);
                }
                return reviewReportReasonRepository;
            }
        }

        public IReviewFeedbackRepository ReviewFeedbackRepository
        {
            get
            {
                if (reviewFeedbackRepository == null)
                {
                    reviewFeedbackRepository = new ReviewFeedbackRepository(_context);
                }
                return reviewFeedbackRepository;
            }
        }

        public IReviewMediaRepository ReviewMediaRepository
        {
            get
            {
                if (reviewMediaRepository == null)
                {
                    reviewMediaRepository = new ReviewMediaRepository(_context);
                }
                return reviewMediaRepository;
            }
        }

        protected override AuditLog ConvertAuditEntry(AuditEntry entry)
        {
            return entry.ToAudit().Adapt<AuditLog>();
        }
    }
}