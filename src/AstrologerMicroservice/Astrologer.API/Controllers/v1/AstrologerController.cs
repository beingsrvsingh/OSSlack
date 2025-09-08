using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Domain.Entities.Enums;
using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;

namespace Astrologer.API.Controllers.v1
{
    public class AstrologerController : BaseController
    {
        [HttpPost("register-astrologer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddAstrologer([FromBody] CreateAstrologerCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            // Assuming result contains no location or resource URI, just return Created with no body
            return Created(string.Empty, new { Message = "Astrologer created successfully." });
        }

        // Update Astrologer
        [HttpPut("update-astrologer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAstrologer([FromBody] UpdateAstrologerCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Astrologer updated successfully." });
        }

        // Delete Astrologer by Id
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAstrologer(int id)
        {
            var result = await Mediator.Send(new DeleteAstrologerCommand(id));

            if (!result.Succeeded)
                return NotFound(new { Message = "Astrologer not found or could not be deleted." });

            return Ok(new { Message = "Astrologer deleted successfully." });
        }

        // Get Astrologer by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAstrologerById(int id)
        {
            var result = await Mediator.Send(new GetAstrologerByIdQuery(id));

            if (!result.Succeeded || result.Data == null)
                return NotFound(new { Message = "Astrologer not found." });

            return Ok(result.Data);
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