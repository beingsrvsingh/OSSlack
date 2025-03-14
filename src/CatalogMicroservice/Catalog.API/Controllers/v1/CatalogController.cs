using BaseApi;
using Catalog.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.v1
{
    [AllowAnonymous]
    public class CatalogController : BaseController
    {
        private readonly ISampleMongoRepository sampleMongoRepository;

        public CatalogController(ISampleMongoRepository sampleMongoRepository)
        {
            this.sampleMongoRepository = sampleMongoRepository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = sampleMongoRepository.FindByIdAsync("65e2c27d151738aef3353115");
            return Ok(result);
        }
    }
}
