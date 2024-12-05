using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.CartItem;
using MRC_API.Payload.Response.Order;
using MRC_API.Service.Interface;
using Repository.Enum;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    public class OrderController : BaseController<OrderController>
    {
        private readonly IOrderService _orderService;
        public OrderController(ILogger<OrderController> logger, IOrderService orderService) : base(logger)
        {
            _orderService = orderService;
        }
        [HttpPost(ApiEndPointConstant.Order.CreateNewOrder)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateOrder([FromForm] CreateOrderRequest createOrderRequest)
        {
           
            var createOrderResponse  = await _orderService.CreateOrder(createOrderRequest);
            if(createOrderResponse.status == StatusCodes.Status400BadRequest.ToString())
            {
                return BadRequest(createOrderResponse);
            }
            if (createOrderResponse.status == StatusCodes.Status404NotFound.ToString())
            {
                return NotFound(createOrderResponse);
            }
            return CreatedAtAction(nameof(CreateOrder), createOrderResponse);
        }
        [HttpGet(ApiEndPointConstant.Order.GetListOrder)]
        [ProducesResponseType(typeof(IPaginate<ApiResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListOrder([FromQuery] int? page, [FromQuery] int? size, [FromQuery] bool? isAscending = null)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _orderService.GetListOrder(pageNumber, pageSize, isAscending);
            if (response == null || response.data == null)
            {
                return Problem(detail: MessageConstant.OrderMessage.OrderIsEmpty, statusCode: StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }
        [HttpGet(ApiEndPointConstant.Order.GetALLOrder)]
        [ProducesResponseType(typeof(IPaginate<ApiResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetALLOrder([FromQuery] int? page,
                                                    [FromQuery] int? size,
                                                    [FromQuery] string? status,
                                                    [FromQuery] bool? isAscending = null,
                                                    [FromQuery]  string userName = null)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _orderService.GetAllOrder(pageNumber, pageSize,status, isAscending, userName);
            if (response == null || response.data == null)
            {
                return Problem(detail: MessageConstant.OrderMessage.OrderIsEmpty, statusCode: StatusCodes.Status404NotFound);
            }
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Order.GetOrderById)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetOrder([FromRoute] Guid id)
        {
            var response = await _orderService.GetOrderById(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpPut(ApiEndPointConstant.Order.UpdateOrder)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateOrder([FromRoute] Guid id, [FromQuery] OrderStatus? orderStatus, [FromQuery]ShipEnum? shipStatus)
        {
            var response = await _orderService.UpdateOrder(id, orderStatus, shipStatus);
            return StatusCode(int.Parse(response.status), response);
        }

    }
}
