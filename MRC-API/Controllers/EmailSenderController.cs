using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant; // Assuming this contains MessageConstant class
using MRC_API.Payload.Request.Email;
using MRC_API.Service.Interface;

namespace MRC_API.Controllers
{
    public class SendEmailController : BaseController<SendEmailController>
    {
        private readonly IEmailSender _emailSender;

        public SendEmailController(ILogger<SendEmailController> logger, IEmailSender emailSender) : base(logger)
        {
            _emailSender = emailSender;
        }

        [HttpPost(ApiEndPointConstant.Email.SendEmail)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> SendEmail([FromBody] EmailRequest emailRequest)
        {
            if (emailRequest is null || !ModelState.IsValid) // Use ModelState for validation
            {
                return BadRequest(MessageConstant.EmailMessage.InvalidEmailRequest);
            }

            try
            {
                await _emailSender.SendVerificationEmailAsync(emailRequest.Email, emailRequest.Otp);
                return Ok(MessageConstant.EmailMessage.EmailSentSuccessfully);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while sending email.");
                return Problem(MessageConstant.EmailMessage.EmailSendFail);
            }
        }
    }
}