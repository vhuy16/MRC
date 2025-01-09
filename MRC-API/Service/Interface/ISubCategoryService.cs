using MRC_API.Payload.Request.SubCategory;
using MRC_API.Payload.Response;

namespace MRC_API.Service.Interface
{
    public interface ISubCategoryService
    {
        Task<ApiResponse> CreateSubCategory(CreateSubCategoryRequest createSubCategoryRequest);
        Task<ApiResponse> GetSubCategories(int page, int size);
        Task<ApiResponse> UpdateSubCategory(Guid id, UpdateSubCategoryRequest updateSubCategoryRequest);
        Task<ApiResponse> DeleteSubCategory(Guid id);
        Task<ApiResponse> GetSubCategory(Guid id);
    }
}
