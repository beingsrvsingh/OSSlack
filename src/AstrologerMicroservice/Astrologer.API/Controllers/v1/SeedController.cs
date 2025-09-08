using AstrologerMicroservice.Application.Features.Admin.Commands;
using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace AstrologerMicroservice.API.Controllers.v1
{
    public class SeedsController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-astrologer-languages")]
        public async Task<IActionResult> SeedAstrologerLanguage()
        {
            // new Language { Id = 1, Name = "English" },
            // new Language { Id = 2, Name = "Hindi" },
            // new Language { Id = 3, Name = "Sanskrit" },
            // new Language { Id = 4, Name = "Tamil" }
            var result = await Mediator.Send(new SeedAstrologerLanguageCommand());

            if (result.Succeeded)
            {
                return Created(string.Empty, result.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}