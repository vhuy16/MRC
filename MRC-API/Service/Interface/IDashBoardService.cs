using MRC_API.Payload.Response;

namespace MRC_API.Service.Interface
{
    public interface IDashBoardService
    {
        Task<ApiResponse> GetDashBoard(int? month, int? year);
    }
}
