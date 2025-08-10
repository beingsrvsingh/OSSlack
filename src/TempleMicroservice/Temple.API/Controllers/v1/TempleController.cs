using Temple.Application.Features.Commands;
using Temple.Application.Features.Query;
using Temple.Domain.Entities.Enums;
using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace Temple.API.Controllers.v1
{
    public class TempleController : BaseController
    {
        [HttpPost("register-Temple")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTemple([FromBody] CreateTempleCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            // Assuming result contains no location or resource URI, just return Created with no body
            return Created(string.Empty, new { Message = "Temple created successfully." });
        }

        // Update Temple
        [HttpPut("update-Temple")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTemple([FromBody] UpdateTempleCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Temple updated successfully." });
        }

        // Delete Temple by Id
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTemple(int id)
        {
            var result = await Mediator.Send(new DeleteTempleCommand(id));

            if (!result.Succeeded)
                return NotFound(new { Message = "Temple not found or could not be deleted." });

            return Ok(new { Message = "Temple deleted successfully." });
        }

        // Get Temple by Id
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTempleById(int id)
        {
            var result = await Mediator.Send(new GetTempleByIdQuery(id));

            if (!result.Succeeded || result.Data == null)
                return NotFound(new { Message = "Temple not found." });

            return Ok(result.Data);
        }

        // Search Temples (example with optional query params)
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchTemples(
            [FromQuery] string? language = null,
            [FromQuery] string? expertise = null,
            [FromQuery] ConsultationMode? consultationMode = null,
            [FromQuery] bool? isActive = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetSearchTemplesQuery
            {
                Language = language,
                Expertise = expertise,
                ConsultationMode = consultationMode,
                IsActive = isActive,
                Page = page,
                PageSize = pageSize
            };

            var result = await Mediator.Send(query);

            return Ok(result.Data);
        }
    }
}