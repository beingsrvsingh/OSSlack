using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Review.Application.Features.Commands;

namespace Review.API.Controllers.v1
{
    public class SupportController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> SaveReviewAction(ReviewActionCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }
    }
}
