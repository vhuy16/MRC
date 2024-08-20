using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Response.Order;

namespace MRC_API.Service.Interface
{
    public interface IOrderService
    {
        Task<CreateOrderResponse> CreateOrder( List<OrderDetailRequest> orderDetailRequests);
    }
}
