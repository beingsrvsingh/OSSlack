using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SecretManagement.Application.Features.SecretManager.Commands;
using SecretManagement.Application.Features.SecretManager.Queries;

namespace SecretManagementService.Controllers
{
    public class SecretsManagerController : BaseController
    {

        [HttpGet("secrets")]
        public async Task<IActionResult> GetAllSecrets()
        {
            var userId = User?.Identity?.Name ?? string.Empty;

            var query = new GetAllSecretsQuery(userId);

            var response = await Mediator.Send(query);

            if (response == null || !response.Succeeded || response.Data == null)
                return NotFound("No credentials found.");

            return Ok(response.Data);
        }

        [HttpGet("{appName}/{environment}/{key}")]
        public async Task<IActionResult> GetSecret(string appName, string environment, string key)
        {
            var query = new GetSecretQuery(appName, environment, key);

            var result = await Mediator.Send(query);

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSecret([FromBody] CreateSecretKeyCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Accepted(new { Message = "Secret creation triggered successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSecret([FromBody] UpdateSecretKeyCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Accepted(new { Message = "Secret update triggered successfully." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecretKeyCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Accepted(new { Message = "Secret deletion triggered successfully." });
        }
    }
}