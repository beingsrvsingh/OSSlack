using BaseApi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Commands;
using Review.Application.Features.Queries;

namespace Review.API.Controllers.v1
{
    [Authorize]
    public class ReviewController : BaseController
    {
        //60*60 - 1 Hour
        [ResponseCache(Duration = (60 * 60), Location = ResponseCacheLocation.Any, VaryByHeader = "User-Agent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet, Route("GetReviewByProduct")]
        public async Task<IActionResult> GetReview([FromQuery] GetReviewByProductQuery query)
        {
            return new OkObjectResult(await Mediator.Send(query));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> SaveReview(ReviewCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost, Route("Feedback")]
        public async Task<IActionResult> SaveReviewFeedback(ReviewDetailHelpFulCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost, Route("Report")]
        public async Task<IActionResult> SaveReport(ReviewReportDetailCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
