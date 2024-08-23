namespace MRC_API.Payload.Response.Order
{
    public class GetOrderResponse
    {
        public Guid? OrderId {get;set;}
        public decimal? totalPrice { get; set; }
        public List<OrderDetailCreateResponseModel> OrderDetails { get; set; } = new List<OrderDetailCreateResponseModel>();

        public class OrderDetailCreateResponseModel
        {
            public string? productName { get; set; }
            public decimal? price { get; set; }
            public int? quantity { get; set; }

        }
    }
}
