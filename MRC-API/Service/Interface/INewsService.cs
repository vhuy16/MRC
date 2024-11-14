using MRC_API.Payload.Response;

namespace MRC_API.Service.Interface
{
    public interface INewsService
    {
        Task<ApiResponse> CreateNewsFromExternalSource(string sourceUrl);
    }
}
