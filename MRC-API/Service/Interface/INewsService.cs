using MRC_API.Payload.Request.News;
using MRC_API.Payload.Response;
using Repository.Enum;

namespace MRC_API.Service.Interface
{
    public interface INewsService
    {
        Task<ApiResponse> CreateNewsFromExternalSource(string sourceUrl);
        Task<ApiResponse> CreateNews(CreateNewsRequest request);
        Task<ApiResponse> GetAllNews(int page, int size, TypeNewsEnum? type, Guid? ignoredId);
        Task<ApiResponse> GetNewsById(Guid id);
        Task<ApiResponse> DeleteNews(Guid id);
        Task<ApiResponse> UpdateNews(Guid id, UpdateNewsRequest request);
    }
}
