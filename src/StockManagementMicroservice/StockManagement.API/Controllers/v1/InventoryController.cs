using BaseApi;
using Microsoft.AspNetCore.Mvc;
using StockManagement.Application.Features.Commands;
using StockManagement.Application.Features.EventHandlers.Commands;
using StockManagement.Application.Features.Query;

namespace StockManagement.API.Controllers.v1
{
    public class InventoryController : BaseController
    {
        // ======= STOCK =======

        [HttpGet("stock/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetStockById(int id)
        {
            var result = await Mediator.Send(new GetStockByIdQuery(id));
            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost("stock")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStock([FromBody] AddStockCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Stock added successfully." });
        }

        // ======= WAREHOUSE =======

        [HttpGet("warehouses")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var result = await Mediator.Send(new GetAllWarehousesQuery());
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost("warehouse")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddWarehouse([FromBody] AddWarehouseCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Warehouse added successfully." });
        }

        // ======= STOCK TRANSACTIONS =======

        [HttpGet("stock/{stockId}/transactions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetStockTransactions(int stockId)
        {
            var result = await Mediator.Send(new GetStockTransactionsQuery(stockId));
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // ======= STOCK ALERTS =======

        [HttpGet("stock-alerts/active")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetActiveStockAlerts()
        {
            var result = await Mediator.Send(new GetActiveStockAlertsQuery());
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // ======= STOCK ADJUSTMENTS =======

        [HttpPost("stock-adjustments")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddStockAdjustment([FromBody] AddStockAdjustmentCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Stock adjustment added successfully." });
        }
    }

}