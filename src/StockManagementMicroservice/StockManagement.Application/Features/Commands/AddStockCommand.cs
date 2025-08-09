using MediatR;
using Shared.Utilities.Response;
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Features.Commands
{
    public record AddStockCommand(StockDto Stock) : IRequest<Result>;

}