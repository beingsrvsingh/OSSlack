using BaseApi;
using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;

namespace CartMicroservice.API.Controllers.v1
{
    public class CartController : BaseController
    {
        // GET api/cart/user/{userId}
        [HttpGet("user/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartByUserId(string userId)
        {
            var result = await Mediator.Send(new GetCartByUserIdQuery(userId));

            if (!result.Succeeded || result.Data == null)
                return NotFound(new { Message = "Cart not found for user." });

            return Ok(result.Data);
        }

        // GET api/cart/{cartId}/items
        [HttpGet("{cartId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCartWithItems(int cartId)
        {
            var result = await Mediator.Send(new GetCartWithItemsQuery(cartId));

            if (!result.Succeeded || result.Data == null)
                return NotFound(new { Message = "Cart not found." });

            return Ok(result.Data);
        }

        // POST api/cart/item
        [HttpPost("item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartDto cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddCartItemCommand(cart);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Cart item add successfully." });
        }

        // PUT api/cart/item
        [HttpPut("item")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartDto cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new UpdateCartItemCommand(cart);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Cart item updated successfully." });
        }

        // DELETE api/cart/item/{cartItemId}
        [HttpDelete("item/{cartItemId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCartItem(int cartItemId)
        {
            var result = await Mediator.Send(new RemoveCartItemCommand(cartItemId));

            if (!result.Succeeded)
                return NotFound(new { Message = "Cart item not found or could not be removed." });

            return Ok(new { Message = "Cart item removed successfully." });
        }

        // DELETE api/cart/{cartId}/clear
        [HttpDelete("{cartId}/clear")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ClearCartItems(int cartId)
        {
            var result = await Mediator.Send(new ClearCartItemsCommand(cartId));

            if (!result.Succeeded)
                return NotFound(new { Message = "Cart not found or could not be cleared." });

            return Ok(new { Message = "Cart cleared successfully." });
        }
    }

}