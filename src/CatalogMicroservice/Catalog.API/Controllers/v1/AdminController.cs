using BaseApi;
using Catalog.Application.Features.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    [Authorize]
    public class AdminController : BaseController
    {
        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            return new OkObjectResult(await Mediator.Send(new GetCatalogQuery()));
        }
    }
}
