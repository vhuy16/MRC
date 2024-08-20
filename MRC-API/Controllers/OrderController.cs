using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Request.User;
using MRC_API.Payload.Response.Order;
using MRC_API.Payload.Response.User;
using MRC_API.Service.Implement;
using MRC_API.Service.Interface;

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



    }
}
