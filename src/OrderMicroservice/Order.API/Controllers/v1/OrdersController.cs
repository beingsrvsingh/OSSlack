using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Commands;
using Order.Application.Features.Query;

namespace Order.API.Controllers.v1
{
    public class OrderController : BaseController
    {
        // GET api/order/{orderId}
        [HttpGet("{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            var result = await Mediator.Send(new GetOrderByIdQuery(orderId));

            if (!result.Succeeded)
                return NotFound(new { Message = "Order not found." });

            return Ok(result);
        }

        [HttpGet("summary/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderDetails(string orderId)
        {
            var result = await Mediator.Send(new GetOrderDetailQuery(orderId));

            if (!result.Succeeded)
                return NotFound(new { Message = "Order not found." });

            return Ok(result);
        }

        [HttpGet("trending-products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTrendingProductsByCid([FromQuery]GetTrendingProductQuery query)
        {
            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return NotFound(new { Message = "Order not found." });

            return Ok(result);
        }

        // POST api/order
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] AddOrderCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Created(string.Empty, new { Message = "Order created successfully." });
        }

        // List Orders by UserId or all if no UserId provided
        [HttpGet("list-orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ListOrders([FromQuery] string? userId = null)
        {
            var query = new ListOrdersQuery(userId);
            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);

            return Ok(result);
        }

        // Update Order
        [HttpPut("update-order")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, result.Errors);

            return Ok(new { Message = "Order updated successfully." });
        }

        // Delete Order
        [HttpDelete("delete-order/{orderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteOrder([FromRoute] int orderId)
        {
            var command = new DeleteOrderCommand(orderId);
            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(new { Message = "Order deleted successfully." });
        }
    }

}
