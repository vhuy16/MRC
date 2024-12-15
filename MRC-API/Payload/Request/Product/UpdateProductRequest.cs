namespace MRC_API.Payload.Request.Product
{
    public class UpdateProductRequest
    {
        public string? ProductName { get; set; } // Optional: Product name
        public Guid? CategoryId { get; set; } // Optional: Category ID
        public int? Quantity { get; set; } // Optional: Quantity
        
        public decimal? Price { get; set; }
        public string? Description { get; set; } // Optional: Description
        public string? Message { get; set; }
        public string? Status { get; set; }
        public List<IFormFile>? ImageLink { get; set; } = new List<IFormFile>(); // Optional: List of image URLs
    }
}
