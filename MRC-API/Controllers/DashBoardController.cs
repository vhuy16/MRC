using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Infrastructure;
using MRC_API.Payload.Response;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;

namespace MRC_API.Controllers
{
    public class DashBoardController : BaseController<DashBoardController>
    {
        private readonly IDashBoardService _dashBoardService;
        public DashBoardController(ILogger<DashBoardController> logger, IDashBoardService dashBoardService) : base(logger)
        {
            _dashBoardService = dashBoardService;
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpGet(ApiEndPointConstant.DashBoard.GetDashBoard)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetDashBoard([FromQuery] int? month, [FromQuery] int? year)
        {
            var response = await _dashBoardService.GetDashBoard(month, year);

            return StatusCode(int.Parse(response.status), response);
        }
    }
}
