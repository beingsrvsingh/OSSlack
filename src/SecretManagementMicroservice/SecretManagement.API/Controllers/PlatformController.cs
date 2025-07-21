
using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Platform.Queries;
using SecretManagement.Application.Queries;

namespace SecretManagement.API.Controllers;

public class PlatformController : BaseController
{

    [HttpGet("credentials")]
    public async Task<IActionResult> GetAllCredentials()
    {
        var response = await Mediator.Send(new GetAllCredentialsQuery());
        if (response == null)
            return NotFound("No credentials found.");
        return Ok(response);
    }

    [HttpGet("credentials/{keyName}")]
    public async Task<IActionResult> GetCredential([FromQuery] string keyName)
    {
        if (string.IsNullOrEmpty(keyName))
            return BadRequest("Key name cannot be null or empty.");

        var response = await Mediator.Send(new GetCredentialByKeyQuery(keyName));
        if (response == null)
            return NotFound($"Credential with key '{keyName}' not found.");

        return Ok(response);
    }

    [HttpPost]
    [Route("add-credentials")]
    public async Task<IActionResult> CreateEnvironment([FromBody] CreateEnvironmentCommand command)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await Mediator.Send(command);
        return Accepted(new { Message = "Environment creation triggered successfully." });
    }

    [HttpDelete("credentials/{keyName}")]
    public async Task<IActionResult> RemoveCredential(string keyName)
    {
        if (string.IsNullOrWhiteSpace(keyName))
            return BadRequest("Key name cannot be null or empty.");

        await Mediator.Send(new RemoveCredentialCommand(keyName));
        return NoContent();
    }
}