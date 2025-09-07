using BaseApi;
using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kathavachak.API.Controllers.v1
{
    public class KathavachakCategoryController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateKathavachakCategory([FromBody] CreateKathavachakCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Category created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateKathavachakCategory([FromBody] UpdateKathavachakCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Category updated successfully." });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteKathavachakCategory(int id)
        {
            var result = await Mediator.Send(new DeleteKathavachakCategoryCommand(id));

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Category deleted successfully." });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetKathavachakCategoryById(int id)
        {
            var result = await Mediator.Send(new GetKathavachakCategoryByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllKathavachakCategories()
        {
            var result = await Mediator.Send(new GetAllKathavachakCategoriesQuery());

            return Ok(result);
        }
    }

}
