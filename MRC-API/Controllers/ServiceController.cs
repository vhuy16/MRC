using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Service;
using MRC_API.Payload.Response;
using MRC_API.Service.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MRC_API.Controllers
{
    [ApiController]
    [Route(ApiEndPointConstant.Service.ServiceEndPoint)]
    public class ServiceController : ControllerBase
    {
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServiceController> _logger;

        public ServiceController(ILogger<ServiceController> logger, IServiceService serviceService)
        {
            _logger = logger;
            _serviceService = serviceService;
        }

        [HttpPost(ApiEndPointConstant.Service.CreateNewService)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewService([FromBody] CreateNewServiceRequest createNewServiceRequest)
        {
            var response = await _serviceService.CreateNewService(createNewServiceRequest);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.Service.GetAllService)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllServices([FromQuery] int? page,
                                                 [FromQuery] int? size,
                                                 [FromQuery] string searchName = null,
                                                 [FromQuery] bool? isAscending = null)
        {
            var response = await _serviceService.GetAllServices(page ?? 1, size ?? 10, searchName, isAscending);
            return StatusCode(int.Parse(response.status), response);
        }
        [HttpGet(ApiEndPointConstant.Service.GetAllServiceBySatus)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllServiceBySatus([FromQuery] int? page,
                                                 [FromQuery] int? size,
                                                 [FromQuery] string searchName = null,
                                                 [FromQuery] bool? isAscending = null,
                                                 [FromQuery] string status = null)
        {
            var response = await _serviceService.GetAllServicesByStatus(page ?? 1, size ?? 10, searchName, status, isAscending);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.Service.GetService)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetService([FromRoute] Guid id)
        {
            var response = await _serviceService.GetService(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpPut(ApiEndPointConstant.Service.UpdateService)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateService([FromRoute] Guid id, [FromBody] UpdateServiceRequest updateServiceRequest)
        {
            var response = await _serviceService.UpdateService(id, updateServiceRequest);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpDelete(ApiEndPointConstant.Service.DeleteService)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteService([FromRoute] Guid id)
        {
            var response = await _serviceService.DeleteService(id);
            return StatusCode(int.Parse(response.status), response);
        }
    }
}
