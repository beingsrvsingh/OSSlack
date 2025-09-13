using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SearchAggregator.Application.Contracts;
using SearchAggregator.Application.Contracts.Dtos;
using SearchAggregator.Application.Features.Command;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Application.Services;
using SearchAggregator.Domain.Entities;
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

        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AddUserSearchHistory([FromBody] AddSearchUserHistoryCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.UserId) || string.IsNullOrWhiteSpace(request.Query))
            {
                return BadRequest("UserId and Query are required.");
            }

            var added = await Mediator.Send(request);

            if (added != null)
                return Ok(added);

            return StatusCode(500, "Failed to add search history.");
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

        [HttpGet("top-global")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTopGlobalSearches([FromQuery] int topN = 5)
        {
            try
            {
                var query = new GetTopGlobalSearchesQuery(topN);
                var result = await Mediator.Send(query);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching top global searches");
                return StatusCode(500, "Internal server error");
            }
        }

    }
}