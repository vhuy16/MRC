using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Service;
using MRC_API.Payload.Response.Service;
using MRC_API.Service.Interface;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    public class ServiceController : BaseController<ServiceController>
    {
        private readonly IServiceService _serviceService;
        public ServiceController(ILogger<ServiceController> logger, IServiceService serviceService) : base(logger)
        {
            _serviceService = serviceService;
        }

        [HttpPost(ApiEndPointConstant.Service.CreateNewService)]
        [ProducesResponseType(typeof(CreateNewServiceResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewService([FromBody] CreateNewServiceRequest createNewServiceRequest)
        {
            var createNewServiceResponse = await _serviceService.CreateNewService(createNewServiceRequest);
            if (createNewServiceResponse == null)
            {
                return Problem(MessageConstant.ServiceMessage.CreateServiceFail);
            }
            return CreatedAtAction(nameof(CreateNewService), createNewServiceResponse);
        }

        [HttpGet(ApiEndPointConstant.Service.GetAllService)]
        [ProducesResponseType(typeof(IPaginate<GetServiceResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllService([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _serviceService.GetAllServices(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.ServiceMessage.ServiceIsEmpty);
            }
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Service.GetService)]
        [ProducesResponseType(typeof(GetServiceResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetService([FromRoute] Guid id)
        {
            var response = await _serviceService.GetService(id);
            if (response == null)
            {
                return Problem(MessageConstant.ServiceMessage.ServiceNotExist);
            }
            return Ok(response);
        }

        [HttpPut(ApiEndPointConstant.Service.UpdateService)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateService([FromRoute] Guid id, [FromBody] UpdateServiceRequest updateServiceRequest)
        {
            var response = await _serviceService.UpdateService(id, updateServiceRequest);
            return Ok(response);
        }

        [HttpDelete(ApiEndPointConstant.Service.DeleteService)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteService([FromRoute] Guid id)
        {
            var response = await _serviceService.DeleteService(id);
            return Ok(response);
        }
    }
}
