namespace MRC_API.Payload.Response.Order
{
    public class CreateOrderResponse
    {
        public decimal? totalPrice {  get; set; }
        public List<OrderDetailCreateResponse> OrderDetails { get; set; } = new List<OrderDetailCreateResponse>();
        
        public class OrderDetailCreateResponse
        {
            public string? productName { get; set; }
            public decimal? price { get; set; }
            public int? quantity { get; set; }
         
        }
    }
}
