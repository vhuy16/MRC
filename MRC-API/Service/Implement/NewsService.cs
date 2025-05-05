using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.News;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.News;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;

namespace MRC_API.Service.Implement
{
    public class NewsService : BaseService<News>, INewsService
    {
        private readonly HttpClient _httpClient;
        private const string ExternalNewsApiUrl = "https://externalnewsapi.com/api/news";
        private readonly HtmlSanitizerUtils _sanitizer;


        public NewsService(IUnitOfWork<MrcContext> unitOfWork, ILogger<News> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, HttpClient httpClient, HtmlSanitizerUtils sanitizer) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            httpClient = _httpClient;
            _sanitizer = sanitizer;
        }

        public async Task<ApiResponse> CreateNewsFromExternalSource(string sourceUrl)
        {
            //try
            //{
            //    var externalNewsResponse = await _httpClient.GetFromJsonAsync<ExternalNewsResponse>(sourceUrl);
            //    if (externalNewsResponse == null || !externalNewsResponse.Articles.Any())
            //    {
            //        return new ApiResponse { status = "error", message = "No news found from the source.", data = null };
            //    }

            //    foreach (var article in externalNewsResponse.Articles)
            //    {
            //        var existingNews = await _unitOfWork.GetRepository<News>().SingleOrDefaultAsync(predicate: n => n.Title == article.Title);
            //        if (existingNews != null) continue;

            //        var newsEntity = new News
            //        {
            //            Id = Guid.NewGuid(),
            //            Title = article.Title,
            //            Content = article.Content,
            //            SourceName = article.Source,
            //            DatePublished = article.PublishedAt,
            //            Status = StatusEnum.Available.GetDescriptionFromEnum(),

            //        };

            //        await _unitOfWork.GetRepository<News>().InsertAsync(newsEntity);
            //    }

            //    var isSuccessful = await _unitOfWork.CommitAsync() > 0;
            //    return new ApiResponse
            //    {
            //        status = StatusCodes.Status201Created.ToString(),
            //        message = "News successfully created from external source.",
            //        data = null
            //    };
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error creating news from external source.");
            //    return new ApiResponse
            //    {
            //        status = "error",
            //        message = "Failed to create news from external source.",
            //        data = null
            //    };
            //}
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> CreateNews(CreateNewsRequest request)
        {
            News news = new News()
            {
                Id = Guid.NewGuid(),
                Content = _sanitizer.Sanitize(request.Content),
                Type = request.GetDescriptionFromEnum(),
                IsActive = true,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
            };

            await _unitOfWork.GetRepository<News>().InsertAsync(news);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Tạo tin tức thành công",
                    data = new CreateNewsResponse()
                    {
                        Content = news.Content,
                        Type = news.Type
                    }
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status400BadRequest.ToString(),
                message = "Tạo tin tức thất bại",
                data = null
            };
        }

        public async Task<ApiResponse> GetAllNews(int page, int size, TypeNewsEnum type)
        {
            var news = await _unitOfWork.GetRepository<News>().GetPagingListAsync(
                selector: n => new GetNewsResponse()
                {
                    Id = n.Id,
                    Content = n.Content,
                    Type = n.Type,
                },
                predicate: n => n.IsActive == true && n.Type.Equals(type.GetDescriptionFromEnum()),
                orderBy: n => n.OrderByDescending(n => n.InsDate),
                page: page,
                size: size);

            int totalItems = news.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (news == null || news.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "News retrieved successfully.",
                    data = new Paginate<News>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<News>()
                    }
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "News retrieved successfully.",
                data = news
            };
        }

        public async Task<ApiResponse> GetNewsById(Guid id)
        {
            var news = await _unitOfWork.GetRepository<News>().SingleOrDefaultAsync(
                selector: n => new GetNewsResponse()
                {
                    Id = n.Id,
                    Content = n.Content,
                    Type = n.Type,
                },
                predicate: n => n.Id.Equals(id));
            if(news == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Không tìm thấy tin tức này",
                    data = null
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Tin tức",
                data = news
            };
        }

        public async Task<ApiResponse> DeleteNews(Guid id)
        {
            var news = await _unitOfWork.GetRepository<News>().SingleOrDefaultAsync(
                predicate: n => n.Id.Equals(id));
            if (news == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Không tìm thấy tin tức này",
                    data = null
                };
            }

            news.IsActive = false;
            news.DelDate = TimeUtils.GetCurrentSEATime();

            _unitOfWork.GetRepository<News>().UpdateAsync(news);
            await _unitOfWork.CommitAsync();

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Xóa tin tức thành công",
                data = true
            };
        }
    }
}
