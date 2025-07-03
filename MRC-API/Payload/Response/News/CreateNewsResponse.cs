using Repository.Enum;

namespace MRC_API.Payload.Response.News
{
    public class CreateNewsResponse
    {
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? Type { get; set; }
    }
}
