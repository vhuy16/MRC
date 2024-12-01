using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Infrastructure;
using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Request.Form;
using MRC_API.Payload.Response;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;

namespace MRC_API.Controllers
{
    [ApiController]
    [Route(ApiEndPointConstant.Form.FormEndPoint)]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly ILogger<FormController> _logger;

        public FormController(ILogger<FormController> logger, IFormService formService)
        {
            _logger = logger;
            _formService = formService;
        }

        [HttpPost(ApiEndPointConstant.Form.SentForm)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewBooking([FromBody] CreateFormRequest request)
        {
            var response = await _formService.CreateForm(request);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpGet(ApiEndPointConstant.Form.GetForms)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetForms([FromQuery] int? page, [FromQuery] int? size, [FromQuery] string? serviceType)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _formService.GetForms(pageNumber, pageSize, serviceType);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpGet(ApiEndPointConstant.Form.GetForm)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetForm([FromRoute] Guid id)
        {
            var response = await _formService.GetForm(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpDelete(ApiEndPointConstant.Form.DeleteForm)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteForm([FromRoute] Guid id)
        {
            var response = await _formService.DeleteForm(id);
            return StatusCode(int.Parse(response.status), response);
        }
    }
}
