namespace MRC_API.Payload.Response.Dashboard
{
    public class GetDashBoardResponse
    {
        public int TotalUsers {  get; set; }
        public int TotalCategories { get; set; }
        public List<CategoryDetail> Categories { get; set; }
        public int TotalProducts { get; set; }
        public List<ProductDetail> Products { get; set; }
        public int? TotalOrder { get; set; }
        public decimal? TotalRevenue { get; set; }
        public class CategoryDetail
        {
            public string? CategoryName { get; set; }
        }

        public class ProductDetail
        {
            public string? ProductName { get; set; }
            public int? Quantity { get; set; }
            public decimal? Price { get; set; }
        }
    }
    
}
