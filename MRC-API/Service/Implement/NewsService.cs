using AutoMapper;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.News;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;

namespace MRC_API.Service.Implement
{
    public class NewsService : BaseService<News>, INewsService
    {
        private readonly HttpClient _httpClient;
        private const string ExternalNewsApiUrl = "https://externalnewsapi.com/api/news";


        public NewsService(IUnitOfWork<MrcContext> unitOfWork, ILogger<News> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, HttpClient httpClient) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            httpClient = _httpClient;
        }

        public async Task<ApiResponse> CreateNewsFromExternalSource(string sourceUrl)
        {
            try
            {
                var externalNewsResponse = await _httpClient.GetFromJsonAsync<ExternalNewsResponse>(sourceUrl);
                if (externalNewsResponse == null || !externalNewsResponse.Articles.Any())
                {
                    return new ApiResponse { status = "error", message = "No news found from the source.", data = null };
                }

                foreach (var article in externalNewsResponse.Articles)
                {
                    var existingNews = await _unitOfWork.GetRepository<News>().SingleOrDefaultAsync(predicate: n => n.Title == article.Title);
                    if (existingNews != null) continue;

                    var newsEntity = new News
                    {
                        Id = Guid.NewGuid(),
                        Title = article.Title,
                        Content = article.Content,
                        SourceName = article.Source,
                        DatePublished = article.PublishedAt,
                        Status = StatusEnum.Available.GetDescriptionFromEnum(),

                    };

                    await _unitOfWork.GetRepository<News>().InsertAsync(newsEntity);
                }

                var isSuccessful = await _unitOfWork.CommitAsync() > 0;
                return new ApiResponse
                {
                    status = StatusCodes.Status201Created.ToString(),
                    message = "News successfully created from external source.",
                    data = null
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating news from external source.");
                return new ApiResponse
                {
                    status = "error",
                    message = "Failed to create news from external source.",
                    data = null
                };
            }
        }
    }
}
