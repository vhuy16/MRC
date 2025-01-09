namespace MRC_API.Payload.Request.SubCategory
{
    public class UpdateSubCategoryRequest
    {
        public Guid? CategoryId { get; set; }

        public string? SubCategoryName { get; set; }
    }
}
