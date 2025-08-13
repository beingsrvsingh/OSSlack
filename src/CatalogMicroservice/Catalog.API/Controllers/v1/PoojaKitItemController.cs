using BaseApi;
using Catalog.Application.Features.Commands;
using Catalog.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    public class PoojaKitItemController : BaseController
    {
        [HttpPost("create-poojakit-item")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePoojaKitItem([FromBody] CreatePoojaKitItemCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Pooja kit item created successfully." });
        }

        [HttpPut("update-poojakit-item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePoojaKitItem([FromBody] UpdatePoojaKitItemCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(new { Message = "Pooja kit item updated successfully." });
        }

        [HttpDelete("delete-poojakit-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePoojaKitItem(int id)
        {
            var result = await Mediator.Send(new DeletePoojaKitItemCommand(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("poojakit-items/{poojaKitId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetItemsByPoojaKitId(int poojaKitId)
        {
            var result = await Mediator.Send(new GetPoojaKitItemByIdQuery(poojaKitId));
            return Ok(result);
        }

        [HttpGet("poojakit-item/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoojaKitItemById(int id)
        {
            var result = await Mediator.Send(new GetPoojaKitItemByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }
    }
}