using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Priest.Application.Features.Commands;
using Priest.Application.Features.Query;
using Shared.Utilities.Response;

namespace Priest.API.Controllers.v1
{
    public class PriestController : BaseController
    {
        // Create Priest
        [HttpPost("register-priest")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddPriest([FromBody] CreatePriestCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Priest created successfully." });
        }

        // Update Priest
        [HttpPut("update-priest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePriest([FromBody] UpdatePriestCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Priest updated successfully." });
        }

        // Delete Priest
        [HttpDelete("delete-priest/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePriest(int id)
        {
            var result = await Mediator.Send(new DeletePriestCommand { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Priest deleted successfully." });
        }

        // Get Priest by ID
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPriestById(int id)
        {
            var result = await Mediator.Send(new GetPriestByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("by-subcategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPriestBySubcategoryIdAsync([FromQuery] string scid)
        {
            var query = new GetPriestsBySubcategoryIdQuery() { SubCategoryId = scid };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost("filter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetFilterePriestsAsync([FromBody] GetFilteredQuery query)
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

        // Get Filtered Priests (by Language and Expertise)
        [HttpGet("filter-priest")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFilteredPriests([FromQuery] string? language, [FromQuery] string? expertise)
        {
            var query = new GetFilteredPriestsQuery
            {
                Language = language,
                Expertise = expertise
            };

            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        // Get Priest Languages
        [HttpGet("{priestId:int}/languages")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPriestLanguages(int priestId)
        {
            var result = await Mediator.Send(new GetPriestLanguagesQuery { PriestId = priestId });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        // Get Priest Expertise
        [HttpGet("{priestId:int}/expertise")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPriestExpertise(int priestId)
        {
            var result = await Mediator.Send(new GetPriestExpertiseQuery { PriestId = priestId });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        // Get Schedules By Priest ID
        [HttpGet("{priestId:int}/schedules")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSchedulesByPriestId(int priestId)
        {
            var result = await Mediator.Send(new GetSchedulesByPriestIdQuery { PriestId = priestId });

            if (!result.Succeeded)
                return NotFound(result);

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