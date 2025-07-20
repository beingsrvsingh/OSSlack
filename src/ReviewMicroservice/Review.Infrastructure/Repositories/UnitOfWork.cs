using Review.Domain.Core.UOW;
using Review.Domain.Repository;
using Review.Infrastructure.Persistence.Context;
using Review.Infrastructure.Repositories;
using Shared.Infrastructur.UoW;
using System.Data;

namespace Shared.Infrastructure.Repositories
{
    public class UnitOfWork : BaseUnitOfWork<ReviewDbContext>, IUnitOfWork
    {
        public UnitOfWork(ReviewDbContext dbContext) : base(dbContext)
        { }

        private IReviewRepository? reviewRepository;
        private IReviewDetailRepository? reviewReportInfoRepository;
        private IReportLookUpRepository? reportLookUpRepository;

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

        public IReviewDetailRepository ReviewReportDetailRepository
        {
            get
            {
                if (reviewReportInfoRepository == null)
                {
                    reviewReportInfoRepository = new ReviewDetailRepository(_context);
                }
                return reviewReportInfoRepository;
            }
        }

        public IReportLookUpRepository ReviewReportLookUpRepository
        {
            get
            {
                if (reportLookUpRepository == null)
                {
                    reportLookUpRepository = new ReportLookUpRepository(_context);
                }
                return reportLookUpRepository;
            }
        }        
    }
}