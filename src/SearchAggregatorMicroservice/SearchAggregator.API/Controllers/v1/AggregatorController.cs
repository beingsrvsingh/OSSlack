using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Domain.Entities;

namespace SearchAggregator.API.Controllers.v1
{
    public class AggregatorController : BaseController
    {
        [HttpGet("trending-product")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingProductAsync()
        {
            var result = await Mediator.Send(new GetTrendingProductQuery());
            return Ok(result);
        }

        [HttpGet("trending-astrologer")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingAstrologerAsync()
        {
            var result = await Mediator.Send(new GetTrendingAstrologerQuery());
            return Ok(result);
        }

        [HttpGet("trending-pooja")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingPoojaAsync()
        {
            var result = await Mediator.Send(new GetTrendingPoojaQuery());
            return Ok(result);
        }

        [HttpGet("trending-priest")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingPriestAsync()
        {
            var result = await Mediator.Send(new GetTrendingPriestQuery());
            return Ok(result);
        }

        [HttpGet("trending-temple")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingTempleAsync()
        {
            var result = await Mediator.Send(new GetTrendingTempleQuery());
            return Ok(result);
        }

        [HttpGet("trending-kathavachak")]
        [ProducesResponseType(typeof(List<UserSearchHistory>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetTrendingKathavachakAsync()
        {
            var result = await Mediator.Send(new GetTrendingKathavachakQuery());
            return Ok(result);
        }
    }
}
