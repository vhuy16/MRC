using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface ICategoryService
    {
        Task<CreateNewCategoryResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest);
        Task<IPaginate<GetCategoryResponse>> GetAllCategory(int page, int size);
        Task<GetCategoryResponse> GetCategory(Guid id);
        Task<bool> UpdateCategory(Guid id, UpdateCategoryRequest updateCategoryRequest);
        Task<bool> DeleteCategory(Guid id);
    }
}
