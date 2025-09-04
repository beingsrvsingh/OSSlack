using BaseApi;
using CatalogUI.Application.Contracts;
using CatalogUI.Application.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

public class LayoutController : BaseController
{
    private readonly ILayoutService _layoutService;

    public LayoutController(ILayoutService layoutService)
    {
        _layoutService = layoutService;
    }

    // GET api/layout
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LayoutDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<LayoutDto>>> GetAll()
    {
        var layouts = await _layoutService.GetAllLayoutsAsync();
        return Ok(layouts);
    }

    // GET api/layout/{id}
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(LayoutDto), 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<LayoutDto>> GetById(int id)
    {
        var layout = await _layoutService.GetLayoutByIdAsync(id);
        if (layout == null)
            return NotFound($"Layout with ID {id} not found.");

        return Ok(layout);
    }

    // GET api/layout/page/{pageName}
    [HttpGet("page/{pageName}")]
    [ProducesResponseType(typeof(IEnumerable<LayoutDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<LayoutDto>>> GetByPage(string pageName)
    {
        var layouts = await _layoutService.GetLayoutsByPageAsync(pageName);
        return Ok(layouts);
    }

    // GET api/layout/tenant/{tenantId}/role/{userRole?}
    [HttpGet("tenant/{tenantId}/role/{userRole?}")]
    [ProducesResponseType(typeof(IEnumerable<LayoutDto>), 200)]
    [ProducesResponseType(500)]
    public async Task<ActionResult<IEnumerable<LayoutDto>>> GetByTenantAndRole(string tenantId, string? userRole = null)
    {
        var layouts = await _layoutService.GetLayoutsByTenantAndRoleAsync(tenantId, userRole);
        return Ok(layouts);
    }

    // POST api/layout
    [HttpPost]
    [ProducesResponseType(typeof(CreateLayoutDto), 201)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Create([FromBody] CreateLayoutDto layout)
    {
        if (layout == null)
            return BadRequest("Layout cannot be null.");

        bool success = await _layoutService.AddLayoutAsync(layout);
        if (!success)
            return StatusCode(500, "Failed to create layout.");

        return Ok("Layout is created successfully");
    }

    // PUT api/layout/{id}
    [HttpPut("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateLayoutDto layout)
    {
        if (layout == null || layout.Id != id)
            return BadRequest("Layout ID mismatch.");

        bool success = await _layoutService.UpdateLayoutAsync(layout);
        if (!success)
            return NotFound($"Layout with ID {id} not found or update failed.");

        return NoContent();
    }

    // DELETE api/layout/{id}
    [HttpDelete("{id:int}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(int id)
    {
        bool success = await _layoutService.DeleteLayoutAsync(id);
        if (!success)
            return NotFound($"Layout with ID {id} not found or delete failed.");

        return NoContent();
    }
}