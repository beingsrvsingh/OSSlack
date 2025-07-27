using BaseApi;
using Identity.Application.Contracts;
using Identity.Application.Features.Admin.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain.Contracts;

namespace Identity.API.Controllers.v1
{
    public class LocationsController : BaseController
    {
        [HttpGet("country")]
        [ProducesResponseType(typeof(PaginatedResult<CountryResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCountries([FromQuery] GetAllCountriesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}