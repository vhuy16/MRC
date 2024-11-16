namespace MRC_API.Service.Interface
{
    public interface IVNPayService
    {
        Task<string> CreatePaymentUrl(Guid orderId, decimal shippingFee);
    }
}
