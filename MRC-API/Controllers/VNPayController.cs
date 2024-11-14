using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;
using Net.payOS.Types;

namespace MRC_API.Controllers
{
    public class VNPayController : BaseController<VNPayController>
    {
        private readonly IVNPayService _vnPayService;
        public VNPayController(ILogger<VNPayController> logger, IVNPayService vnPayService) : base(logger)
        {
            _vnPayService = vnPayService;
        }

        // Endpoint to create a VNPay payment URL
        [HttpPost(ApiEndPointConstant.VNPay.CreatePaymentUrl)]
        [ProducesResponseType(typeof(CreatePaymentResult), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] Guid orderId)
        {
            try
            {
                var result = await _vnPayService.CreatePaymentUrl(orderId);
                if (string.IsNullOrEmpty(result))
                {
                    return Problem(MessageConstant.PaymentMessage.CreatePaymentFail);
                }
                return Ok(new { PaymentUrl = result });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating VNPay payment URL.");
                return Problem(MessageConstant.PaymentMessage.CreatePaymentFail);
            }
        }

    }
}
