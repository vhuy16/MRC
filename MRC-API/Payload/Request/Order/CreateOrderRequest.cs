namespace MRC_API.Payload.Request.Order
{
    public class CreateOrderRequest
    {
        public List<Guid> CartItem { get; set; }
        public int ShipCost { get; set; }
    }
}
