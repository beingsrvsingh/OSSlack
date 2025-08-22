using BaseApi;
using Catalog.Application.Features.Commands;
using Catalog.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    public class CategoryController : BaseController
    {
        [HttpPost("create-category")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Category created successfully." });
        }

        [HttpPut("update-category")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpDelete("delete-category/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await Mediator.Send(new DeleteCategoryCommand(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all-categories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await Mediator.Send(new GetAllCategoriesQuery());
            return Ok(result);
        }

        [HttpGet("category/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await Mediator.Send(new GetCategoryByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("category/{categoryId:int}/localizations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLocalizedTexts(int categoryId)
        {
            var result = await Mediator.Send(new GetCategoryLocalizedTextsQuery(categoryId));
            return Ok(result);
        }

        [HttpPost("category/add-or-update-localization")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrUpdateLocalization([FromBody] AddOrUpdateCategoryLocalizedTextCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(result);
        }

        [HttpGet("sub-category/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSubCategoryById(int id)
        {
            var result = await Mediator.Send(new GetSubCategoryByCategoryIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("{id:int}/attributes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAttributesByCategoryId(int id)
        {
            var result = await Mediator.Send(new GetAttributesByCategoryIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

    }

}