using MRC_API.Payload.Request.Service;
using MRC_API.Payload.Response.Service;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IServiceService
    {
        Task<CreateNewServiceResponse> CreateNewService(CreateNewServiceRequest createNewServiceRequest);
        Task<IPaginate<GetServiceResponse>> GetAllServices(int page, int size);
        Task<GetServiceResponse> GetService(Guid id);
        Task<bool> UpdateService(Guid id, UpdateServiceRequest updateServiceRequest);
        Task<bool> DeleteService(Guid id);
    }
}
