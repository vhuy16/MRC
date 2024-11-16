using Azure;
using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.CartItem;
using MRC_API.Payload.Response.Pay;
using MRC_API.Service.Interface;
using Net.payOS.Types;

namespace MRC_API.Controllers
{
    public class PayController : BaseController<PayController>
    {
        private readonly IPayService _payService;

        public PayController(ILogger<PayController> logger, IPayService payService) : base(logger)
        {
            _payService = payService;
        }

        // Endpoint to create a payment URL
        [HttpPost(ApiEndPointConstant.Payment.CreatePaymentUrl)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] Guid orderID)
        {
            var result = await _payService.CreatePaymentUrlRegisterCreator(orderID);
            return StatusCode(int.Parse(result.status), result);
        }

        // Endpoint to get payment details
        [HttpGet(ApiEndPointConstant.Payment.GetPaymentInfo)]
        [ProducesResponseType(typeof(ExtendedPaymentInfo), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetPaymentInfo([FromRoute] string paymentLinkId)
        {
            try
            {
                var result = await _payService.GetPaymentInfo(paymentLinkId);
                if (result == null)
                {
                    return Problem(MessageConstant.PaymentMessage.PaymentNotFound);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving payment information.");
                return Problem(MessageConstant.PaymentMessage.PaymentNotFound);
            }
        }
    }
}
