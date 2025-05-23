namespace BasicPaymentGateway.Common.RequestModel
{
    public class InitiatePaymentRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal Amount { get; set; } 
    }
}
