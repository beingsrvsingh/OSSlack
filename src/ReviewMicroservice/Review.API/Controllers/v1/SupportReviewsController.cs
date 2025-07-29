using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Contracts;
using Review.Application.Features.Commands;
using Review.Application.Features.Queries;
using Shared.Utilities.Response;

public class SupportReviewController : BaseController
{
    [HttpGet("user/{UserId}")]
    [ProducesResponseType(typeof(Result<List<ReviewDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUserReviews([FromRoute] GetReviewsByUserQuery query, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        query.Page = page;
        query.PageSize = pageSize;

        var result = await Mediator.Send(query);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("report-resolution")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResolveReport([FromBody] SupportResolveReportCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }
}
