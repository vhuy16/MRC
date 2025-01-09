namespace MRC_API.Payload.Response.SubCategory
{
    public class CreateSubCategoryResponse
    {
        public Guid CategoryId { get; set; }

        public string SubCategoryName { get; set; } = null!;
    }
}
