using BaseApi;
using Catalog.Application.Features.Commands;
using Catalog.Application.Features.EventHandlers.Commands;
using Catalog.Application.Features.EventHandlers.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    public class ServiceSubCategoryController : BaseController
    {
        [HttpPost("create-subcategory")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSubCategory([FromBody] CreateSubCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Subcategory created successfully." });
        }

        [HttpPut("update-subcategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSubCategory([FromBody] UpdateSubCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpDelete("delete-subcategory/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var result = await Mediator.Send(new DeleteSubCategoryCommand(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all-subcategories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSubCategories()
        {
            var result = await Mediator.Send(new GetAllSubCategoriesQuery());
            return Ok(result);
        }

        [HttpGet("subcategory/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var result = await Mediator.Send(new GetSubCategoryByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("subcategory/{subCategoryId:int}/localizations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLocalizedTexts(int subCategoryId)
        {
            var result = await Mediator.Send(new GetSubCategoryLocalizedTextsQuery(subCategoryId));
            return Ok(result);
        }

        [HttpPost("subcategory/add-or-update-localization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrUpdateLocalization([FromBody] AddOrUpdateSubCategoryLocalizedTextCommand request)
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