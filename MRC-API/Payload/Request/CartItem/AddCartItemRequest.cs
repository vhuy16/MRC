namespace MRC_API.Payload.Request.CartItem
{
    public class AddCartItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
