using Logging.Application.Service;
using Logging.Domain.Contracts;
using Logging.Domain.Entities;
using Logging.Domain.Repositories;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Contracts;

namespace Logging.Infrastructure.Service
{
    public class LogService : ILogService
    {
        private readonly ILoggerService<LogService> logger;
        private readonly IAndroidLogRepository androidRepository;
        private readonly IIOSLogRepository iOSRepository;
        private readonly IWebServiceLogRepository webRepository;

        public LogService(ILoggerService<LogService> logger, IAndroidLogRepository androidRepository, IIOSLogRepository iOSRepository, IWebServiceLogRepository webRepository)
        {
            this.logger = logger;
            this.androidRepository = androidRepository;
            this.iOSRepository = iOSRepository;
            this.webRepository = webRepository;
        }

        // WRITE: Android
        public async Task<bool> LogAndroidAsync(AndroidLog log)
        {
            try
            {
                log.Timestamp = DateTime.UtcNow;
                await androidRepository.AddAsync(log);
                await androidRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("[Android] Log write failed: " + ex.Message, ex);
                return false;
            }
        }

        // WRITE: iOS
        public async Task<bool> LogIOSAsync(IOSLog log)
        {
            try
            {
                log.Timestamp = DateTime.UtcNow;
                await iOSRepository.AddAsync(log);
                await iOSRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("[iOS] Log write failed: " + ex.Message, ex);
                return false;
            }
        }

        // WRITE: WebService
        public async Task<bool> LogWebServiceAsync(WebServiceLog log)
        {
            try
            {
                log.Timestamp = DateTime.UtcNow;
                await webRepository.AddAsync(log);
                await webRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("[Web] Log write failed: " + ex.Message, ex);
                return false;
            }
        }

        // READ
        public async Task<PaginatedResult<AndroidLogDto>> GetAndroidLogsAsync(int page, int pageSize)
        {
            return await androidRepository.GetFilteredAsync(page, pageSize);
        }

        public async Task<PaginatedResult<IOSLogDto>> GetIOSLogsAsync(int page, int pageSize)
        {
            return await iOSRepository.GetFilteredAsync(page, pageSize);
        }

        public async Task<PaginatedResult<WebServiceLogDto>> GetWebServiceLogsAsync(int page, int pageSize)
        {
            return await webRepository.GetFilteredAsync(page, pageSize);
        }
    }

}
