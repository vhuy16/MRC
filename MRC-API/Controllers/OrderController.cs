using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.Order;
using MRC_API.Payload.Response.Product;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;
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
        [ProducesResponseType(typeof(CreateOrderResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateOrder([FromBody] List<OrderDetailRequest> orderDetailRequests)
        {
           
           CreateOrderResponse createOrderResponse  = await _orderService.CreateOrder(orderDetailRequests);
            if (createOrderResponse == null)
            {
                return Problem(MessageConstant.UserMessage.CreateUserAdminFail);
            }
            return CreatedAtAction(nameof(CreateOrder), createOrderResponse);
        }
        [HttpGet(ApiEndPointConstant.Order.GetListOrder)]
        [ProducesResponseType(typeof(IPaginate<GetOrderResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetListOrder([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _orderService.GetListOrder(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.OrderMessage.OrderIsEmpty);
            }
            return Ok(response);
        }


    }
}
