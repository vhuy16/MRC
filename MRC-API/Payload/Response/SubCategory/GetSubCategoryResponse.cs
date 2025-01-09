namespace MRC_API.Payload.Response.SubCategory
{
    public class GetsubCategoryResponse
    {
        public Guid CategoryId { get; set; }

        public string SubCategoryName { get; set; } = null!;
    }
}
