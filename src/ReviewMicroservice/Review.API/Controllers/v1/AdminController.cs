using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Commands;
using Review.Application.Features.Queries;
using Shared.Utilities.Response;

public class AdminController : BaseController
{
    [HttpGet("reported")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReportedReviews([FromQuery] GetReportedReviewsQuery query)
    {
        var result = await Mediator.Send(query);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("moderate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ModerateReview([FromBody] ReviewModerationCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }
}
