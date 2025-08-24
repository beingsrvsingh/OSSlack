using Address.Application.Contracts;
using Address.Application.Features;
using Address.Application.Features.Commands;
using Address.Application.Features.Query;
using Address.Domain.Enums;
using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace Address.API.Controllers.v1
{
    public class AddressController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Address created successfully." });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetAddressByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpGet("owner/{ownerId:int}/{ownerType}")]
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByOwner(int ownerId, AddressOwnerType ownerType)
        {
            var result = await Mediator.Send(new GetAllAddressesByOwnerQuery
            {
                OwnerId = ownerId,
                OwnerType = ownerType
            });

            return Ok(result.Data);
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAddressCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            request.Id = id;
            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteAddressCommand { Id = id });

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(new { Message = "Address deleted successfully." });
        }
    }

}