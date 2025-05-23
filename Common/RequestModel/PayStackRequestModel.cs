namespace BasicPaymentGateway.Common.RequestModel
{
    public class PayStackRequestModel
    {
        public string email { get; set; } = string.Empty;
        public string amount { get; set; } = string.Empty;

        public string first_name { get; set; } = string.Empty;
    }
}
