using BasicPaymentGateway.Common.RequestModel;
using BasicPaymentGateway.Common.ResponseModel;

namespace BasicPaymentGateway.Contracts.Interfaces
{
    public interface IPaymentService
    {
        Task<FetchPaymentResponse> InitiatePayment(InitiatePaymentRequest request);
        Task<FetchPaymentResponse> GetTransactionAsync(long transactionId);
    }
}
