namespace Payment.Application.Contracts
{
    public record CreatePaymentResponse(
    string OrderId,
    string OrderToken,
    decimal Amount
        );

}
