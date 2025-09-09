using Microsoft.EntityFrameworkCore;
using Priest.Domain.Core.Repository;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class ConsultationModeRepository : Repository<ConsultationMode>, IConsultationModeRepository
    {
        private readonly DbContext dbContext;

        public ConsultationModeRepository(DbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
