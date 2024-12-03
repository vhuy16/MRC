namespace MRC_API.Payload.Response.Order
{
    public class GetOrderResponse
    {
        public Guid? OrderId { get; set; }
        public decimal? ShipCost { get; set; }
        public decimal? TotalPayment {  get; set; }
        public decimal? TotalPrice { get; set; }
        public string? Status { get; set; }
        public string? Address { get; set; }
        public List<OrderDetailCreateResponseModel> OrderDetails { get; set; } = new List<OrderDetailCreateResponseModel>();
        public UserResponse User { get; set; } = new UserResponse(); // Added user information property

        public class OrderDetailCreateResponseModel
        {
            public string? ProductName { get; set; }
            public decimal? Price { get; set; }
            public int? Quantity { get; set; }
        }

        public class UserResponse
        {
            public string Name { get; set; }
            public string? FullName {  get; set; }
            public string Email { get; set; }
            public string? PhoneNumber { get; set; }
        }
    }
}
