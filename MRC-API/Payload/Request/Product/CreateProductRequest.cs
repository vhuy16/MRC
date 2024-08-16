using Repository.Entity;

namespace MRC_API.Payload.Request.Product
{
    public class CreateProductRequest
    {
       

        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int Quantity { get; set; }

        public Guid CategoryId { get; set; }

        public virtual ICollection<Image> Images { get; set; } = new List<Image>();
    }
}
