﻿using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.UserAddress
{
    public class UpdateUserAddressCommand : IRequest<Result>
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
