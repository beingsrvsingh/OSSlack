using BaseApi;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.API.Controllers.v1
{
    [Authorize]
    public class AdminController : BaseController
    {
    }
}
