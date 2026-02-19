using BaseApi;
using Cart.Application.Features.Commands;
using CartMicroservice.Application.Contracts;
using CartMicroservice.Application.Features.Commands;
using CartMicroservice.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;

namespace CartMicroservice.API.Controllers.v1
{
    public class CartController : BaseController
    {
        // GET api/cart/user/{userId}
        [HttpGet("{userId}")]
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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartDto cart)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var command = new AddCartItemCommand(cart);

            var result = await Mediator.Send(command);

            return Ok(result);
        }

        [HttpPatch("{productId}/quantity")]
        public async Task<IActionResult> UpdateCartItem(int productId, [FromBody] int quantity)
        {
            var command = new UpdateCartItemCommand(productId, quantity);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Cart item quantity updated successfully." });
        }


        // DELETE api/cart/item/{cartItemId}
        [HttpDelete("{productId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCartItem(int productId)
        {
            var result = await Mediator.Send(new RemoveCartItemCommand(productId));

            if (!result.Succeeded)
                return Ok(new { Message = "Cart item not found or could not be removed." });

            return Ok(new { Message = "Cart item removed successfully." });
        }

        // DELETE api/cart/{cartId}/remove
        [HttpDelete("{cartId}/remove")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveCartItems(int cartId)
        {
            var result = await Mediator.Send(new ClearCartItemsCommand(cartId));

            if (!result.Succeeded)
                return NotFound(new { Message = "Cart not found or could not be cleared." });

            return Ok(new { Message = "Cart cleared successfully." });
        }
    }

}