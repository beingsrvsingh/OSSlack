using BaseApi;
using Identity.Application.Features.Admin.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1.Admin
{
    /// <summary>
    /// This Controller responsible to migrate data to database
    /// </summary>
    [Authorize]
    public class AdminsController : BaseController
    {
        [HttpGet, Route("Country")]
        public async Task<IActionResult> GetAllCountries(ISender sender)
        {
            await Mediator.Send(sender);
            return Ok();
        }

        [HttpPost, Route("AddCountry")]
        public async Task<IActionResult> AddCountries(ISender sender)
        {
            await Mediator.Send(new CountryCommand());
            return Ok();
        }

        [HttpPost, Route("Cities")]
        public async Task<IActionResult> AddCities(ISender sender)
        {
            await Mediator.Send(new CityCommand());
            return Ok();
        }

    }
}
