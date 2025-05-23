namespace BasicPaymentGateway.Contracts.Interfaces
{
    public interface IGenericService
    {
        Task<string> ConsumeRestAPI(string apiEndpoint, string? serializedRequest, Dictionary<string, string> headers, string method);
    }
}
