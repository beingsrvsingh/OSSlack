using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Contracts;
using SearchAggregator.Application.Services;
using Shared.Application.Interfaces.Logging;

namespace SearchAggregator.API.Controllers.v1
{
    public class SearchController : BaseController
    {
        private readonly ISearchAggregatorService _searchService;
        private readonly ILoggerService<SearchController> _logger;

        public SearchController(ISearchAggregatorService searchService, ILoggerService<SearchController> logger)
        {
            _searchService = searchService;
            _logger = logger;
        }

        /// <summary>
        /// Search products, priests, and other microservices with pagination.
        /// </summary>
        /// <param name="query">Search text</param>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Aggregated search results</returns>
        [HttpGet]
        [ProducesResponseType(typeof(SearchResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(
            [FromQuery] GetSearchQuery query,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Query parameter is required.");

            try
            {
                var result = await _searchService.SearchAsync(query.Query, query.Page, query.PageSize, cancellationToken);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while searching for '{Query}'", query);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}