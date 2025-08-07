using BaseApi;
using Catalog.Application.Features.Commands;
using Catalog.Application.Features.Queries;
using Catalog.Application.Features.Queries.QueryHandlers;
using Catalog.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    public class PoojaKitController : BaseController
    {
        [HttpPost("create-poojakit")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePoojaKit([FromBody] CreatePoojaKitCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Pooja kit created successfully." });
        }

        [HttpPut("update-poojakit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePoojaKit([FromBody] UpdatePoojaKitCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpDelete("delete-poojakit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePoojaKit(int id)
        {
            var result = await Mediator.Send(new DeletePoojaKitCommand(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all-poojakits")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPoojaKits()
        {
            var result = await Mediator.Send(new GetAllPoojaKitsQuery());
            return Ok(result);
        }

        [HttpGet("poojakit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoojaKitById(int id)
        {
            var result = await Mediator.Send(new GetPoojaKitByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("poojakit/{kitId:int}/localizations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLocalizedTexts(int kitId)
        {
            var result = await Mediator.Send(new GetPoojaKitLocalizedTextsQuery(kitId));
            return Ok(result);
        }

        [HttpPost("poojakit/add-or-update-localization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrUpdateLocalization([FromBody] AddOrUpdatePoojaKitLocalizedTextCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }
    }
}