using Priest.Domain.Core.Repository;
using Priest.Infrastructure.Persistence.Context;
using PriestMicroservice.Domain.Entities;
using Shared.Infrastructure.Repositories;

namespace Priest.Infrastructure.Persistence.Repository
{
    public class ConsultationModeRepository : Repository<ConsultationMode>, IConsultationModeRepository
    {
        private readonly PriestDbContext dbContext;

        public ConsultationModeRepository(PriestDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }

}
