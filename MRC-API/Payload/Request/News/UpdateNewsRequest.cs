namespace MRC_API.Payload.Request.News;

public class UpdateNewsRequest
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Type { get; set; }
}