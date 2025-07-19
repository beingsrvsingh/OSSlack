using BaseApi;
using SecretManagement.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace SecretManagementService.Controllers
{
    public class SecretsManagerController : BaseController
    {

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
    }
}
