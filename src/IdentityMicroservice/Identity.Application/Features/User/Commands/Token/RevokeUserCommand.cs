﻿using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.Token
{
    public class RevokeUserCommand : IRequest<Result>
    {
        public string Token { get; set; }
        public required string UserId { get; set; }
    }
}
