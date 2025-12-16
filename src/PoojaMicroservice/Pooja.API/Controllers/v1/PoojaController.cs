using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Pooja.Application.Features.Commands;
using Pooja.Application.Features.Queries;
using Pooja.Application.Features.Query;
using Shared.Utilities.Response;

namespace Pooja.API.Controllers.v1
{
    public class PoojaController : BaseController
    {
        // Create Pooja
        [HttpPost("create-pooja")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePooja([FromBody] CreatePoojaCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Pooja created successfully." });
        }

        // Update Pooja
        [HttpPut("update-pooja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePooja([FromBody] UpdatePoojaCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Pooja updated successfully." });
        }

        // Delete Pooja
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePooja(int id)
        {
            var result = await Mediator.Send(new DeletePoojaCommand { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Pooja deleted successfully." });
        }

        // Get Pooja by Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoojaById(int id)
        {
            var result = await Mediator.Send(new GetPoojaByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("by-subcategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoojaBySubcategoryIdAsync([FromQuery] string scid)
        {
            var query = new GetPoojasBySubcategoryIdQuery() { SubCategoryId = scid };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFiltereTemplesAsync([FromBody] GetFilteredPoojasQuery query)
        {
            var result = await Mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result);
            }

            // Return bad request or other appropriate status if failure
            return BadRequest(result);
        }

        [HttpGet("trending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubcategoryTrendingAsync([FromQuery] GetTrendingQuery query)
        {
            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return NotFound(new { Message = "Products not found." });

            return Ok(result);
        }

        // Get All Poojas
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPoojas()
        {
            var result = await Mediator.Send(new GetAllPoojasQuery());
            return Ok(result);
        }

        // Get Poojas by Temple
        [HttpGet("temple/{templeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPoojasByTemple(int templeId)
        {
            var result = await Mediator.Send(new GetPoojasByTempleQuery { TempleId = templeId });
            return Ok(result);
        }

        // Get Poojas by Priest
        [HttpGet("priest/{priestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPoojasByPriest(int priestId)
        {
            var result = await Mediator.Send(new GetPoojasByPriestQuery { PriestId = priestId });
            return Ok(result);
        }

        /// <summary>
        /// Search products by query with pagination
        /// </summary>
        /// <param name="query">Search keyword</param>
        /// <param name="page">Page number (default 1)</param>
        /// <param name="pageSize">Page size (default 10)</param>
        /// <returns>Paginated list of product search results</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Search([FromQuery] GetSearchQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.Query))
                return BadRequest("Query parameter is required.");

            var result = await Mediator.Send(query);

            if (result.Succeeded)
            {
                return Ok(result);
            }
            return NotFound(result);
        }
    }
}
