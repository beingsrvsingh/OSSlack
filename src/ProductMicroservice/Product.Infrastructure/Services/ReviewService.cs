using System.Net.Http.Json;
using Product.Application.Contracts;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly HttpClient _httpClient;
        private readonly ILoggerService<ReviewService> _logger;

        public ReviewService(IHttpClientFactory httpClientFactory, ILoggerService<ReviewService> logger)
        {
            ArgumentNullException.ThrowIfNull(httpClientFactory);

            _httpClient = httpClientFactory.CreateClient(nameof(IReviewService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ReviewSummaryDto> GetProductReviewSummaryAsync(int pid)
        {
            try
            {
                var url = $"Review/product/{pid}/summary";

                var response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<ReviewSummaryDto>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? new ReviewSummaryDto();
                }

                return new ReviewSummaryDto();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Review MS for ProductId: {pid}, SubCategoryId: {SubCategoryId} {IsSummary}", pid);
                // or throw if you want to propagate the error
                return new ReviewSummaryDto();
            }
        }

        public async Task<List<ReviewSummaryDto>> GetProductReviewSummariesAsync(List<int> pids)
        {
            try
            {
                var url = "Review/product/summaries";                

                var response = await _httpClient.PostAsJsonAsync(url, new { pids });

                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadFromJsonAsync<Result<List<ReviewSummaryDto>>>();

                if (result != null && result.Succeeded)
                {
                    return result.Data ?? [];
                }

                return [];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling Review MS for ProductIds: {@ProductIds}", pids);
                return [];
            }
        }

    }
}