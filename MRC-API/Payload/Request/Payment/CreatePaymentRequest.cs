namespace MRC_API.Payload.Request.Payment
{
    public class CreatePaymentRequest
    {
     
        public decimal? Amount { get; set; }
        public Guid UserId { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Status {  get; set; }
        public long OrderCode { get; set; }
        public Guid OrderId { get; set; }
    }
}
