﻿using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Contracts
{
    public class ReviewRequest : IRequest<Result>
    {
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public int Star { get; set; }
        public string Message { get; set; }
    }
}
