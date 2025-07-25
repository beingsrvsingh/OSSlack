using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace SecretManagement.API.Controllers;

public class HealthController : BaseController
{

    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok(new { Status = "Healthy" });
    }

}