using BaseApi;
using SecretManagement.Features.Queries;
using Microsoft.AspNetCore.Mvc;
using SecretManagement.Application.Features.SecretManager.Commands;
using SecretManagement.Application.Features.SecretManager.Queries;
using System.Threading.Tasks;
using MediatR;
using SecretManagement.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using SecretManagement.Domain.Core.Repository;

namespace SecretManagementService.Controllers
{
    public class SecretsManagerController : BaseController
    {
        // private readonly SecretManagementDbContext context;
        // private readonly ISecretRepository secretRepository;

        // public SecretsManagerController(SecretManagementDbContext context)
        // {
        //     this.context = context;
        //     this.secretRepository = secretRepository;
        // }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("security-token")]
        public async Task<IActionResult> GetSecurityToken(GetUserSecurityTokenQuery req)
        {
            var response = await Mediator.Send(req);

            return Ok(response);
        }

        [HttpPost]
        [Route("encrypt")]
        public IActionResult EncryptConnectionString(string connectionString)
        {
            // string keyName = this.registryService.ConnectionStringKeyName;

            // if (!string.IsNullOrEmpty(connectionString)) {
            //     string encryptConnectionString = Cryptography.EncryptString(connectionString);
            //     this.registryService.SetValue(keyName, encryptConnectionString);
            // }

            return Ok();
        }

        [HttpGet]
        [Route("get-encrypted-token-security-key")]
        public IActionResult EncryptTokenSecurityKey()
        {
            // string keyName = this.registryService.TokenSeurityKeyName;

            // if (!string.IsNullOrEmpty(JwtConstant.JWT_TOKEN_SECURITYKEY))
            // {
            //     string encryptConnectionString = Cryptography.EncryptString(JwtConstant.JWT_TOKEN_SECURITYKEY);
            //     this.registryService.SetValue(keyName, encryptConnectionString);
            //     return Ok();
            // }

            return BadRequest();
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

            await Mediator.Send(command); // triggers the notification handler

            return Accepted(new { Message = "Secret creation triggered successfully." });
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSecret([FromBody] UpdateSecretKeyCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(command);
            return Accepted(new { Message = "Secret update triggered successfully." });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSecret([FromBody] DeleteSecretKeyCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await Mediator.Send(command);
            return Accepted(new { Message = "Secret deletion triggered successfully." });
        }
    }
}
