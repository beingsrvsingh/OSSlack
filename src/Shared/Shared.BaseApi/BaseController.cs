using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shared.Application.Common.Services.Interfaces;
using System.Security.Claims;

namespace BaseApi
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase, IUserProvider
    {
        private IMediator? _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

        public string UserId => this.HttpContext.User?.FindFirstValue("Id")!;

        public string UserName => this.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;

        public string Role => this.HttpContext.User?.FindFirstValue(ClaimTypes.Role)!;

        public string Email => this.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }
}
