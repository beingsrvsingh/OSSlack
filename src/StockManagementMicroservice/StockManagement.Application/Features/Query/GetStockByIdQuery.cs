using MediatR;
using Shared.Utilities.Response;

namespace StockManagement.Application.Features.Query
{
    public record GetStockByIdQuery(int StockId) : IRequest<Result>;

}