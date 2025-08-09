using MediatR;
using Shared.Utilities.Response;

namespace StockManagement.Application.Features.Query
{
    public record GetStockTransactionsQuery(int StockId) : IRequest<Result>;

}