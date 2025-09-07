using Shared.Application.Interfaces.Logging;
using Temple.Application.Services;
using Temple.Domain.Core.Repositories;
using Temple.Domain.Entities;

namespace Temple.Infrastructure.Services
{
    public class TempleDonationService : ITempleDonationService
    {
        private readonly ITempleDonationRepository _donationRepository;
        private readonly ILoggerService<TempleDonationService> _logger;

        public TempleDonationService(ITempleDonationRepository donationRepository, ILoggerService<TempleDonationService> logger)
        {
            _donationRepository = donationRepository;
            _logger = logger;
        }

        public async Task<bool> CreateAsync(TempleDonation donation)
        {
            try
            {
                await _donationRepository.AddAsync(donation);
                await _donationRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in CreateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> UpdateAsync(TempleDonation donation)
        {
            try
            {
                await _donationRepository.DeleteAsync(donation);
                await _donationRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in UpdateAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var entity = await _donationRepository.GetByIdAsync(id);
                if (entity == null) return false;

                await _donationRepository.DeleteAsync(entity);
                await _donationRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in DeleteAsync: {ex.Message}", ex);
                return false;
            }
        }

        public async Task<TempleDonation?> GetByIdAsync(int id)
        {
            return await _donationRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<TempleDonation>> GetAllAsync()
        {
            return await _donationRepository.GetAllAsync();
        }
    }

}
