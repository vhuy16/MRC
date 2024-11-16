using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Pay;
using Net.payOS.Types;

namespace MRC_API.Service.Interface
{
    public interface IPayService
    {
        Task<ExtendedPaymentInfo> GetPaymentInfo(string paymentLinkId);
        Task<ApiResponse> CreatePaymentUrlRegisterCreator(Guid orderId);
    }
}
