using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;
using Temple.Application.Features.Commands;
using Temple.Application.Features.Queries;

namespace Temple.API.Controllers.v1
{
    public class TempleController : BaseController
    {
        // GET api/TempleMaster/{id}
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTempleMasterByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetTempleByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        // POST api/TempleMaster/create
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTempleMaster([FromBody] CreateTempleMasterCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Temple master created successfully." });
        }

        // PUT api/TempleMaster/update/{id}
        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTempleMaster(int id, [FromBody] UpdateTempleMasterCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest("Id in route does not match Id in request body.");

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Temple master updated successfully." });
        }

        // DELETE api/TempleMaster/delete/{id}
        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteTempleMaster(int id)
        {
            var result = await Mediator.Send(new DeleteTempleMasterCommand(id));

            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Temple master deleted successfully." });
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