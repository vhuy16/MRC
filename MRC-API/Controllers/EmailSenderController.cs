using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
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
            if (emailRequest == null || string.IsNullOrEmpty(emailRequest.Email) || string.IsNullOrEmpty(emailRequest.Subject) || string.IsNullOrEmpty(emailRequest.Message))
            {
                return BadRequest(MessageConstant.EmailMessage.InvalidEmailRequest);
            }

            try
            {
                await _emailSender.SendEmailAsync(emailRequest.Email, emailRequest.Subject, emailRequest.Message);
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
