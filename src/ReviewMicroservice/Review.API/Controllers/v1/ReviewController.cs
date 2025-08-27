using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Contracts;
using Review.Application.Features.Commands;
using Review.Application.Features.Commands.CommandHandlers;
using Review.Application.Features.Queries;
using Shared.Utilities.Response;

public class ReviewController : BaseController
{
    [HttpGet("summary/{ProductId}")]
    [ProducesResponseType(typeof(List<ReviewDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReviewsByProduct([FromRoute] GetReviewsByProductQuery query, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        query.Page = page;
        query.PageSize = pageSize;

        var result = await Mediator.Send(query);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("product/{ProductId}/summary")]
    [ProducesResponseType(typeof(ReviewSummaryDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetReviewSummary([FromRoute] GetProductReviewSummaryQuery query)
    {
        var result = await Mediator.Send(query);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("{ReviewId}")]
    [ProducesResponseType(typeof(ReviewDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetReviewById([FromRoute] GetReviewByIdQuery query)
    {
        var result = await Mediator.Send(query);
        return result.Succeeded
            ? Ok(result)
            : NotFound(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddReview([FromBody] AddReviewCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpDelete("{ReviewId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteReview([FromRoute] int reviewId, [FromQuery] string userId)
    {
        var command = new DeleteReviewCommand { ReviewId = reviewId, RequestingUserId = userId };
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("feedback")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitReviewFeedback([FromBody] ReviewFeedbackCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpPost("report")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(object), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ReportReview([FromBody] ReviewReportCommand command)
    {
        var result = await Mediator.Send(command);
        return result.Succeeded
            ? Ok(result)
            : BadRequest(result);
    }

    [HttpGet("has-marked-helpful")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> HasUserMarkedHelpful([FromQuery] HasUserMarkedReviewHelpfulQuery query)
    {
        query.UserId = UserId;
        var result = await Mediator.Send(query);
        return result.Succeeded ? Ok(result) : BadRequest(result);
    }
}
