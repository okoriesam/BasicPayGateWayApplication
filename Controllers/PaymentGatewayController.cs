using BasicPaymentGateway.Common.RequestModel;
using BasicPaymentGateway.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;


namespace BasicPaymentGateway.Controllers
{
    [Route("api/v1")]
    [EnableRateLimiting("FixedWindowPolicy")]
    [ApiController]
    public class PaymentGatewayController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentGatewayController(IPaymentService paymentService)
        {
            _paymentService = paymentService; 
        }


        [HttpPost]
        [Route("payments")]
        public async Task<IActionResult> Initiate(InitiatePaymentRequest request)
        {
            var InitiatePayment = await _paymentService.InitiatePayment(request);
            return Ok(InitiatePayment);
        }

        [HttpGet]
        [Route("payments/{id}")]
        public async Task<IActionResult> TransactionById(long id)
        {
            var TransactionById = await _paymentService.GetTransactionAsync(id);
            return Ok(TransactionById);
        }
    }
}
