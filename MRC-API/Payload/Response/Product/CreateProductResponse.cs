using Repository.Entity;

namespace MRC_API.Payload.Response.Product
{
    public class CreateProductResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
