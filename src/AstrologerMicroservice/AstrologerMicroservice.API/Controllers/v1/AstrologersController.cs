using AstrologerMicroservice.Application.Features.Commands;
using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Domain.Entities.Enums;
using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace AstrologerMicroservice.API.Controllers.v1
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

        // Search Astrologers (example with optional query params)
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchAstrologers(
            [FromQuery] string? language = null,
            [FromQuery] string? expertise = null,
            [FromQuery] ConsultationMode? consultationMode = null,
            [FromQuery] bool? isActive = true,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 20)
        {
            var query = new GetSearchAstrologersQuery
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