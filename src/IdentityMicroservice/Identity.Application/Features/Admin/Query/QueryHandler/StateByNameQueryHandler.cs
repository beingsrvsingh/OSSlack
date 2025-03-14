﻿using Identity.Application.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Shared.Utilities.Response;

namespace Identity.Application.Features.Admin.Commands
{
    public class StateByNameQueryHandler : IRequestHandler<GetStateByNameQuery, IResult>
    {
        private readonly IAdminService service;
        private readonly IWebHostEnvironment env;

        public StateByNameQueryHandler(IAdminService service, IWebHostEnvironment env)
        {
            this.service = service;
            this.env = env;
        }
        public Task<IResult> Handle(GetStateByNameQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
