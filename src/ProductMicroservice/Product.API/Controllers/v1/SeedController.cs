using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Product.Application.Features.Commands;

namespace Product.API.Controllers.v1
{
    public class SeedController : BaseController
    {

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-product")]
        public async Task<IActionResult> SeedCataglog()
        {
            var result = await Mediator.Send(new SeedCatalogCommand());

            if (result.Succeeded)
            {
                return Created(string.Empty, result.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }

}