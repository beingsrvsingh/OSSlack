using AstrologerMicroservice.Domain.Entities;
using AstrologerMicroservice.Domain.Entities.Enums;
using AstrologerMicroservice.Domain.Repositories;
using AstrologerMicroservice.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace AstrologerMicroservice.Infrastructure.Persistence.Repository
{
    public class AstrologerRepository : Repository<Astrologer>, IAstrologerRepository
    {
        private readonly AstrologerDbContext _context;
        public AstrologerRepository(AstrologerDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<Astrologer>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            return await _context.Astrologers
            .Where(a => a.IsActive)
            .Where(a => a.AstrologerLanguages.Any(l => l.Language.Name == language))
            .Where(a => a.AstrologerExpertises.Any(e => e.Expertise.Name == expertise))
            .Where(a => a.Schedules.Any(s => s.Day == date.DayOfWeek))
            .ToListAsync();
        }

        public async Task<IEnumerable<Astrologer>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.Astrologers
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<Astrologer>> SearchAsync(string? language = null, string? expertise = null, ConsultationModeType? consultationMode = null, bool? isActive = true, int page = 1, int pageSize = 20)
        {
            var query = _context.Astrologers
            .Include(a => a.AstrologerLanguages)
            .Include(a => a.AstrologerExpertises)
            .AsQueryable();

            if (!string.IsNullOrEmpty(language))
            {
                query = query.Where(a => a.AstrologerLanguages.Any(l => l.Language.Name == language));
            }

            if (!string.IsNullOrEmpty(expertise))
            {
                query = query.Where(a => a.AstrologerExpertises.Any(e => e.Expertise.Name == expertise));
            }

            if (consultationMode.HasValue)
            {
                if (consultationMode.HasValue)
                {
                    query = query.Where(a => a.ConsultationModes.Any(cm => cm.ConsultationModeMaster.Mode == consultationMode.Value.ToString()));
                }
            }

            if (isActive.HasValue)
            {
                query = query.Where(a => a.IsActive == isActive.Value);
            }

            return await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<bool> SetExpertisesAsync(int astrologerId, IEnumerable<int> expertiseIds)
        {
            var astrologer = await _context.Astrologers
            .Include(a => a.AstrologerExpertises)
            .FirstOrDefaultAsync(a => a.Id == astrologerId);

            if (astrologer == null) return false;

            // Clear current expertises
            astrologer.AstrologerExpertises.Clear();

            // Load new expertises by IDs and assign
            var expertises = await _context.AstrologerExpertises
                .Where(e => expertiseIds.Contains(e.ExpertiseId))
                .ToListAsync();

            foreach (var exp in expertises)
            {
                astrologer.AstrologerExpertises.Add(exp);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds)
        {
            var astrologer = await _context.Astrologers
            .Include(a => a.AstrologerLanguages)
            .FirstOrDefaultAsync(a => a.Id == astrologerId);

            if (astrologer == null) return false;

            // Clear current languages
            astrologer.AstrologerLanguages.Clear();

            // Load new languages by IDs and assign
            var languages = await _context.AstrologerLanguages
                .Where(l => languageIds.Contains(l.LanguageId))
                .ToListAsync();

            foreach (var lang in languages)
            {
                astrologer.AstrologerLanguages.Add(lang);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Astrologer?> GetAstrologerWithLanguagesAndExpertisesAsync(int id)
        {
            return await _context.Astrologers
                .Include(a => a.AstrologerLanguages)
                .Include(a => a.AstrologerExpertises)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}