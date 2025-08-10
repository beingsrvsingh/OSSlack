using Temple.Domain.Entities;
using Temple.Domain.Entities.Enums;
using Temple.Domain.Repositories;
using Temple.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Shared.Infrastructure.Repositories;

namespace Temple.Infrastructure.Persistence.Repository
{
    public class TempleRepository : Repository<TempleMaster>, ITempleRepository
    {
        private readonly TempleDbContext _context;
        public TempleRepository(TempleDbContext dbContext) : base(dbContext)
        {
            this._context = dbContext;
        }

        public async Task<IEnumerable<TempleMaster>> GetAvailableAsync(DateTime date, string language, string expertise)
        {
            return await _context.TempleMasters
            .Where(a => a.IsActive)
            .Where(a => a.AstrologerLanguages.Any(l => l.Language.Name == language))
            .Where(a => a.TempleExpertises.Any(e => e.Expertise.Name == expertise))
            .Where(a => a.Schedules.Any(s => s.Day == date.DayOfWeek))
            .ToListAsync();
        }

        public async Task<IEnumerable<TempleMaster>> GetAllAsync(int page = 1, int pageSize = 20)
        {
            return await _context.TempleMasters
                .AsNoTracking()
                .OrderBy(a => a.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<TempleMaster>> SearchAsync(string? language = null, string? expertise = null, ConsultationMode? consultationMode = null, bool? isActive = true, int page = 1, int pageSize = 20)
        {
            var query = _context.TempleMasters
            .Include(a => a.AstrologerLanguages)
            .Include(a => a.TempleExpertises)
            .AsQueryable();

            if (!string.IsNullOrEmpty(language))
            {
                query = query.Where(a => a.AstrologerLanguages.Any(l => l.Language.Name == language));
            }

            if (!string.IsNullOrEmpty(expertise))
            {
                query = query.Where(a => a.TempleExpertises.Any(e => e.Expertise.Name == expertise));
            }

            if (consultationMode.HasValue)
            {
                query = query.Where(a => a.ConsultationModes == consultationMode.Value);
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
            var astrologer = await _context.TempleMasters
            .Include(a => a.TempleExpertises)
            .FirstOrDefaultAsync(a => a.Id == astrologerId);

            if (astrologer == null) return false;

            // Clear current expertises
            astrologer.TempleExpertises.Clear();

            // Load new expertises by IDs and assign
            var expertises = await _context.TempleExpertises
                .Where(e => expertiseIds.Contains(e.ExpertiseId))
                .ToListAsync();

            foreach (var exp in expertises)
            {
                astrologer.TempleExpertises.Add(exp);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SetLanguagesAsync(int astrologerId, IEnumerable<int> languageIds)
        {
            var astrologer = await _context.TempleMasters
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

        public async Task<TempleMaster?> GetAstrologerWithLanguagesAndExpertisesAsync(int id)
        {
            return await _context.TempleMasters
                .Include(a => a.AstrologerLanguages)
                .Include(a => a.TempleExpertises)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}