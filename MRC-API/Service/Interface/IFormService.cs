using MRC_API.Payload.Request.Form;
using MRC_API.Payload.Response;

namespace MRC_API.Service.Interface
{
    public interface IFormService
    {
        Task<ApiResponse> CreateForm(CreateFormRequest request);
        Task<ApiResponse> GetForms(int page, int size, string serviceType);
        Task<ApiResponse> GetForm(Guid id);
        Task<ApiResponse> DeleteForm(Guid id);
    }
}
