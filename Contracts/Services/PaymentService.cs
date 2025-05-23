using AutoMapper;
using BasicPaymentGateway.Common.RequestModel;
using BasicPaymentGateway.Common.ResponseModel;
using BasicPaymentGateway.Contracts.Interfaces;
using BasicPaymentGateway.Data;
using BasicPaymentGateway.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace BasicPaymentGateway.Contracts.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericService _genericService;
        private readonly ILogger<PaymentService> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public PaymentService(IGenericService genericService, ILogger<PaymentService> logger, ApplicationDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _genericService = genericService;
            _logger = logger;
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<FetchPaymentResponse> GetTransactionAsync(long transactionId)
        {
            try
            {
                var BaseUrl = _configuration.GetSection("PayStackDetails:PaystackUrl").Value;
                var Key = _configuration.GetSection("PayStackDetails:Test_secret").Value;
                if (BaseUrl == null)
                {
                    throw new Exception("No url or base endpoint provided");
                }
                var endpoint = $"{BaseUrl}transaction/{transactionId}";


                var header = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {Key}" },
                   
                };

                var initiateResponseString = await _genericService.ConsumeRestAPI(endpoint, "", header, "get");
                var initiate = JsonConvert.DeserializeObject<GetTransactionResponse>(initiateResponseString);

                var finalResult = new FetchPaymentResponse
                {
                    message = initiate.message,
                    payment = new Payment
                    {
                        id = initiate.data.id.ToString(),
                         status = initiate.status.ToString(),
                          amount = initiate.data.amount,
                           customer_email = initiate.data.customer.email,
                            customer_name = initiate.data.customer.first_name 
                    }
                };
                return finalResult;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<FetchPaymentResponse> InitiatePayment(InitiatePaymentRequest request)
        {
            try
            {
                var BaseUrl = _configuration.GetSection("PayStackDetails:PaystackUrl").Value;
                var Key = _configuration.GetSection("PayStackDetails:Test_secret").Value;
                if(BaseUrl == null)
                {
                    throw new Exception("No url or base endpoint provided");
                }
                var endpoint = $"{BaseUrl}transaction/initialize";
              

                var header = new Dictionary<string, string>
                {
                    {"Authorization", $"Bearer {Key}" },
                    {"Content-Type", "application/json"},
                };

                var SerializePaystackPayload = new PayStackRequestModel
                {
                    email = request.Email,
                    amount = (request.Amount * 100).ToString(),
                    first_name = request.Name
                };

                var requestBody = JsonConvert.SerializeObject(SerializePaystackPayload);
                var initiateResponseString = await _genericService.ConsumeRestAPI(endpoint, requestBody, header, "Post");

                if (string.IsNullOrWhiteSpace(initiateResponseString))
                {
                    _logger.LogError("Empty response received from Paystack initiation API.");
                    throw new Exception("Empty response from payment gateway initiation.");
                }

                InitiateTransactionResponse initiate = JsonConvert.DeserializeObject<InitiateTransactionResponse>(initiateResponseString);
                if (!initiate.status)
                {
                    _logger.LogWarning("Paystack initiation failed: {Message}", initiate.message);
                    throw new Exception($"Paystack initiation failed: {initiate.message}");
                }
                var reference = initiate.data.reference;
                var verifyEndpoint = $"{BaseUrl}transaction/verify/{reference}";
                var finalresponseString = await _genericService.ConsumeRestAPI(verifyEndpoint, "", header, "get");

                
                if (string.IsNullOrWhiteSpace(finalresponseString))
                {
                    _logger.LogError("Empty response received from Paystack verification API for reference {Reference}.", reference);
                    throw new Exception("Empty response from payment gateway verification.");
                }
                PaystackVerificationResponse fullVerificationResponse = JsonConvert.DeserializeObject<PaystackVerificationResponse>(finalresponseString);
                if (!fullVerificationResponse.status)
                {
                    _logger.LogWarning("Paystack verification failed for reference {Reference}: {Message}", reference, fullVerificationResponse.message);
                    throw new Exception($"Paystack verification failed: {fullVerificationResponse.message}");
                }

                // Ensure data is present in the verification response
                if (fullVerificationResponse.data == null)
                {
                    _logger.LogError("Paystack verification response missing 'data' for reference {Reference}. Response: {Response}", reference, finalresponseString);
                    throw new Exception("Payment gateway verification response did not contain expected data.");
                }
                var fetchPaymentResponse = new FetchPaymentResponse
                { 
                    message = fullVerificationResponse.message,
                    payment = new Payment
                    {
                        id = fullVerificationResponse.data.id.ToString(), 
                        customer_name = request.Name,
                        customer_email = fullVerificationResponse.data.customer?.email,
                        amount = (double)fullVerificationResponse.data.amount / 100, 
                        status = fullVerificationResponse.data.status 
                    }
                };

                var Trans = new Transaction
                {
                    Id = fullVerificationResponse!.data!.id,
                    customer_Name = request.Name,
                    customer_Email = fullVerificationResponse.data.customer.email,
                    amount = fullVerificationResponse.data.amount,
                    status = fullVerificationResponse.status.ToString(),
                    reference = fullVerificationResponse.data.reference,    
                    createdAt = fullVerificationResponse.data.createdAt,
                };

                await _context.Transactions.AddAsync(Trans);
              await  _context.SaveChangesAsync();
                return fetchPaymentResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
