namespace BasicPaymentGateway.Entity
{
    public class Transaction
    {
        public long Id { get; set; }
        public string customer_Name { get; set; } = string.Empty;
        public string customer_Email { get; set;} = string.Empty;
        public double amount { get; set; }
        public string status { get; set; } = string.Empty;
        public string? reference { get; set; }
        public DateTime createdAt { get; set; } 
    }
}
