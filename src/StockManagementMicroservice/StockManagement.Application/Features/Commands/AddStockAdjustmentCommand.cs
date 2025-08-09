using MediatR;
using Shared.Utilities.Response;
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Features.Commands
{
    public record AddStockAdjustmentCommand(StockAdjustmentDto Adjustment) : IRequest<Result>;

}