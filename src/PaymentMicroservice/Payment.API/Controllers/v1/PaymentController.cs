using BaseApi;
using Microsoft.AspNetCore.Mvc;
using PaymentMicroservice.Application.Contracts;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Features.Query;

namespace PaymentMicroservice.API.Controllers.v1
{
    public class PaymentController : BaseController
    {
        // POST: Create Payment Transaction
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> CreatePaymentTransaction([FromBody] AddPaymentTransactionCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Payment transaction created successfully." });
        }

        // GET: Get Payment Logs
        [HttpGet("logs/{transactionId:int}")]
        [ProducesResponseType(typeof(IEnumerable<PaymentTransactionLogDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionLogs([FromRoute] int transactionId)
        {
            var result = await Mediator.Send(new GetTransactionLogsQuery(transactionId));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        // GET: Get Payments by UserId
        [HttpGet("by-user/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<PaymentTransactionDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPaymentsByUserId([FromRoute] string userId)
        {
            var result = await Mediator.Send(new GetPaymentsByUserIdQuery(userId));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        // POST: Create Refund
        [HttpPost("refund")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRefund([FromBody] AddRefundTransactionCommand request)
        {
            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Created(string.Empty, new { Message = "Refund created successfully." });
        }

        // PUT: Update Payment Transaction
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTransaction([FromBody] UpdatePaymentTransactionCommand request)
        {
            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Payment transaction updated successfully." });
        }

        // DELETE: Delete Payment Transaction
        [HttpDelete("delete/{transactionId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction([FromRoute] int transactionId)
        {
            var result = await Mediator.Send(new DeletePaymentTransactionCommand(transactionId));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(new { Message = "Payment transaction deleted successfully." });
        }
    }
}