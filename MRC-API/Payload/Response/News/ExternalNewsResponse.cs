namespace MRC_API.Payload.Response.News
{
    public class ExternalNewsResponse
    {
        public List<ExternalArticle> Articles { get; set; }
    }
    public class ExternalArticle
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public DateTime PublishedAt { get; set; }
    }
}
