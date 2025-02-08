namespace MRC_API.Payload.Response.Product
{
    public class UpdateProductResponse
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string Message { get; set; }
        public int Quantity { get; set; }

        public string SubCategoryName { get; set; }

        public decimal? Price { get; set; }
        public List<string> Images { get; set; } = new List<string>();
    }
}
