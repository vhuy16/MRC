namespace MRC_API.Payload.Request.SubCategory
{
    public class CreateSubCategoryRequest
    {
        public Guid CategoryId { get; set; }

        public string SubCategoryName { get; set; } = null!;
    }
}
