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
                return Conflict(result);

            return Created(string.Empty, result);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetAddressByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("shipping/{userId:int}")]
        [ProducesResponseType(typeof(AddressDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAddressForShippingById(int userId)
        {
            var result = await Mediator.Send(new GetAddressForShippingByIdQuery { Id = userId });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("owner/{userId}/{ownerType}")]
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllByOwner(string userId, AddressOwnerType ownerType)
        {
            var result = await Mediator.Send(new GetAllAddressesByOwnerQuery
            {
                UserId = userId,
                OwnerType = ownerType
            });

            return Ok(result);
        }

        [HttpGet("owner/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<AddressDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByOwner(string userId)
        {
            var result = await Mediator.Send(new GetAddressesByOwnerQuery
            {
                UserId = userId
            });

            return Ok(result);
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
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteAddressCommand { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("address/{id}/mark-default")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkAsDefault(int id)
        {
            var result = await Mediator.Send(new MarkAddressAsDefaultCommand { AddressId = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

    }

}