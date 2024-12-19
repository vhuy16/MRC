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

        [HttpGet("ReturnUrl")]
        public async Task<IActionResult> ReturnUrl()
        {

            // Lấy các tham số từ query string
            string responseCode = Request.Query["code"].ToString();
            string id = Request.Query["id"].ToString();
            string cancel = Request.Query["cancel"].ToString();
            string status = Request.Query["status"].ToString();
            string orderCode = Request.Query["orderCode"];

            if (responseCode == "00" && status == "PAID")
            {
                try
                {

                    var response = await _payService.HandlePaymentCallback(id, long.Parse(orderCode));
                    return Redirect("https://www.mrc.vn/payment/callback?status=success");

                }
                catch (Exception ex)
                {
                    return Problem("Đã xảy ra lỗi: " + ex.Message);
                }
            }
            else if (status == "CANCELLED")
            {
                var response = await _payService.HandlePaymentCallback(id, long.Parse(orderCode));
                return Redirect("https://www.mrc.vn/payment/callback?status=failed");
            }
            else
            {
                return Redirect("https://www.mrc.vn/payment/callback?status=failed");
            }
        }
    }
}
