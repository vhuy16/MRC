using Net.payOS.Types;

namespace MRC_API.Payload.Response.Pay
{
    public record CustomItemData : ItemData
    {
        public Guid ProductId { get; set; }

        public CustomItemData(string name, int quantity, int price, Guid productId)
            : base(name, quantity, price)
        {
            ProductId = productId;
        }
    }


}
