using BaseApi;
using JwtTokenAuthentication.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Domain;
using Utilities.Cryptography;
using Utilities.Interfaces;

namespace Admin.API.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : BaseController
    {
        private readonly IRegistryService registryService;

        public ConfigController(IRegistryService registryService)
        {
            this.registryService = registryService;
        }

        [HttpPost]
        public IActionResult EncryptConnectionString(string connectionString)
        {
            string keyName = this.registryService.ConnectionStringKeyName;

            if (!string.IsNullOrEmpty(connectionString)) {
                string encryptConnectionString = Cryptography.EncryptString(connectionString);
                this.registryService.SetValue(keyName, encryptConnectionString);
            }

            return Ok();
        }

        [HttpGet]
        public IActionResult EncryptTokenSecurityKey()
        {
            string keyName = this.registryService.TokenSeurityKeyName;

            if (!string.IsNullOrEmpty(JwtConstant.JWT_TOKEN_SECURITYKEY))
            {
                string encryptConnectionString = Cryptography.EncryptString(JwtConstant.JWT_TOKEN_SECURITYKEY);
                this.registryService.SetValue(keyName, encryptConnectionString);
                return Ok();
            }

            return BadRequest();
        }
    }
}
