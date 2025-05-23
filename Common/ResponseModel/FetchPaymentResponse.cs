namespace BasicPaymentGateway.Common.ResponseModel
{
    public class FetchPaymentResponse
    {
        public Payment payment { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
    }

    public class Payment
    {
        public string? id { get; set; }
        public object? customer_name { get; set; }
        public string? customer_email { get; set; }
        public double amount { get; set; }
        public string? status { get; set; }
    }
}
