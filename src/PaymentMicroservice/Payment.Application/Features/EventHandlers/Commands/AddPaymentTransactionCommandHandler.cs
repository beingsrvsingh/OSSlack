using MediatR;
using Payment.Application.Services;
using PaymentMicroservice.Application.Features.Commands;
using PaymentMicroservice.Application.Services;
using PaymentMicroservice.Domain.Entities;
using PaymentMicroservice.Domain.Enums;
using Shared.Application.Interfaces.Logging;
using Shared.Domain.Enums;
using Shared.Utilities.Response;

namespace PaymentMicroservice.Application.Features.EventHandlers.Commands
{
    public class AddPaymentTransactionCommandHandler : IRequestHandler<AddPaymentTransactionCommand, Result>
    {
        private readonly IPaymentService _paymentService;
        private readonly ICashfreeService cashfreeService;
        private readonly ILoggerService<AddPaymentTransactionCommandHandler> _logger;

        public AddPaymentTransactionCommandHandler(IPaymentService paymentService, ICashfreeService cashfreeService, ILoggerService<AddPaymentTransactionCommandHandler> logger)
        {
            _paymentService = paymentService;
            this.cashfreeService = cashfreeService;
            _logger = logger;
        }

        public async Task<Result> Handle(AddPaymentTransactionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                //var payment = await cashfreeService.CreateOrderAsync(request.TransactionDto.OrderId,request.TransactionDto.UserId,request.TransactionDto.Amount,request.TransactionDto.Currency);

                var transaction = new PaymentTransaction();

                if (false)
                {
                    transaction.Status = PaymentStatus.Pending;
                }
                else
                {
                    transaction.Status = PaymentStatus.Success;
                }
                
                transaction.OrderId = request.OrderNumber;
                transaction.PaymentReference = Guid.NewGuid().ToString();
                transaction.UserId = request.UserId;
                transaction.Amount = request.Amount;
                transaction.Currency = Enum.Parse<CurrencyCode>(request.Currency, true);

                var success = await _paymentService.AddPaymentTransactionAsync(transaction);

                return success ? Result.Success(new { paymentRefNum = transaction.PaymentReference, Status = transaction.Status}) : Result.Failure(new FailureResponse("FAILED", "Failed to create payment transaction."));
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in AddPaymentTransactionCommandHandler", ex);
                return Result.Failure(new FailureResponse("Exception", ex.Message));
            }
        }
    }

}