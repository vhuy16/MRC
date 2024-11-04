using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Category;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface ICategoryService
    {
        Task<ApiResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest);
        Task<ApiResponse> GetAllCategory(int page, int size);
        Task<ApiResponse> GetCategory(Guid id);
        Task<ApiResponse> UpdateCategory(Guid id, UpdateCategoryRequest updateCategoryRequest);
        Task<ApiResponse> DeleteCategory(Guid id);
    }
}
