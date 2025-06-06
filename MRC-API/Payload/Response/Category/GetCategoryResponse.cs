using MRC_API.Payload.Response.SubCategory;

namespace MRC_API.Payload.Response.Category
{
    public class GetCategoryResponse
    {
        public Guid? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public ICollection<GetsubCategoryResponse> SubCategories { get; set; }
    }
}
