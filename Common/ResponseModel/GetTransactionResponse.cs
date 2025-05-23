﻿namespace BasicPaymentGateway.Common.ResponseModel
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Authorizations
    {
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string card_type { get; set; }
        public string bank { get; set; }
        public string country_code { get; set; }
        public string brand { get; set; }
        public bool reusable { get; set; }
        public string signature { get; set; }
        public object account_name { get; set; }
    }

    public class Customers
    {
        public int id { get; set; }
        public object? first_name { get; set; }
        public object last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public object phone { get; set; }
        public Metadata metadata { get; set; }
        public string risk_action { get; set; }
        public object international_format_phone { get; set; }
    }

    public class CustomField
    {
        public string display_name { get; set; }
        public string variable_name { get; set; }
        public string value { get; set; }
    }

    public class Datar
    {
        public long id { get; set; }
        public string domain { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public object receipt_number { get; set; }
        public int amount { get; set; }
        public object message { get; set; }
        public string gateway_response { get; set; }
        public object helpdesk_link { get; set; }
        public object paid_at { get; set; }
        public object created_at { get; set; }
        public string channel { get; set; }
        public string currency { get; set; }
        public string ip_address { get; set; }
        public string metadata { get; set; }
        public Log log { get; set; }
        public object fees { get; set; }
        public object fees_split { get; set; }
        public Authorizations authorization { get; set; }
        public Customers customer { get; set; }
        public Plan plan { get; set; }
        public Subaccounts subaccount { get; set; }
        public Splits split { get; set; }
        public object order_id { get; set; }
        public object paidAt { get; set; }
        public object createdAt { get; set; }
        public int requested_amount { get; set; }
        public object pos_transaction_data { get; set; }
        public Source source { get; set; }
        public object fees_breakdown { get; set; }
        public object connect { get; set; }
    }

    public class History
    {
        public string type { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }

    public class Log
    {
        public int start_time { get; set; }
        public int time_spent { get; set; }
        public int attempts { get; set; }
        public int errors { get; set; }
        public bool success { get; set; }
        public bool mobile { get; set; }
        public List<object> input { get; set; }
        public List<History> history { get; set; }
    }

    public class Metadata
    {
        public List<CustomField> custom_fields { get; set; }
    }

    public class Plan
    {
    }

    public class GetTransactionResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public Datar data { get; set; }
    }

    public class Source
    {
        public string type { get; set; }
        public string source { get; set; }
        public object identifier { get; set; }
    }

    public class Splits
    {
    }

    public class Subaccounts
    {
    }


}
