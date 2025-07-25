using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Platform.Queries;
using SecretManagement.Application.Queries;

namespace SecretManagement.API.Controllers;

public class PlatformController : BaseController
{
    [HttpGet]
    public async Task<IActionResult> GetAllCredentials()
    {
        var response = await Mediator.Send(new GetAllCredentialsQuery());
        if (response == null)
            return NotFound("No credentials found.");
        return Ok(response);
    }

    [HttpGet("{keyName}")]
    public async Task<IActionResult> GetCredential(string keyName)
    {
        if (string.IsNullOrEmpty(keyName))
            return BadRequest("Key name cannot be null or empty.");

        var response = await Mediator.Send(new GetCredentialByKeyQuery(keyName));
        return Ok(response);
    }

    [HttpPost]
    [Route("add-credentials")]
    public async Task<IActionResult> CreateEnvironment([FromBody] CreateEnvironmentCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var response = await Mediator.Send(command);
        return Accepted(response);
    }

    [HttpDelete("{keyName}")]
    public async Task<IActionResult> RemoveCredential(string keyName)
    {
        if (string.IsNullOrWhiteSpace(keyName))
            return BadRequest("Key name cannot be null or empty.");

        var response = await Mediator.Send(new RemoveCredentialCommand(keyName));
        return Ok(response);
    }
}