using BookingMicroservice.Application.Features.Commands;
using BookingMicroservice.Application.Features.Query;
using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;

namespace Booking.API.Controllers.v1
{
    public class BookingController : BaseController
    {
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Booking([FromBody] CreateBookingCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            // Assuming result contains no location or resource URI, just return Created with no body
            return Created(string.Empty, result);
        }

        // Get Available Slots
        [HttpGet("{id:int}/slots")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBookingsByDate(int id, DateTime date)
        {
            var result = await Mediator.Send(new GetBookingsByDateQuery(id, date));

            if (!result.Succeeded || result.Data == null)
                return NotFound(new { Message = "No available slots found for today." });

            return Ok(result.Data);
        }
    }
}