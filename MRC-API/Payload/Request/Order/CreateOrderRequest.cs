namespace MRC_API.Payload.Request.Order
{
    public class CreateOrderRequest
    {
        public string PaymentUrl { get; set; }
        public Guid PaymentId { get; set; }
    }
}
