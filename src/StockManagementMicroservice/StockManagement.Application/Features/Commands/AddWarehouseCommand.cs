using MediatR;
using Shared.Utilities.Response;
using StockManagement.Application.Contracts;

namespace StockManagement.Application.Features.EventHandlers.Commands
{
    public record AddWarehouseCommand(WarehouseDto Warehouse) : IRequest<Result>;

}