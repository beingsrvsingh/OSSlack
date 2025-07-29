using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Commands;
using Shared.Utilities.Response;

namespace Review.API.Controllers.v1
{
    public class SeedController : BaseController
    {
        [HttpPost("review-report-reasons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SeedReviewReportReasons()
        {
            var result = await Mediator.Send(new SeedReviewReportReasonCommand());

            return result.Succeeded
                ? Ok(result)
                : BadRequest(result);
        }
    }
}