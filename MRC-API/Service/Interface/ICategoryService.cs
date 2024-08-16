using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;

namespace MRC_API.Service.Interface
{
    public interface ICategoryService
    {
        Task<CreateNewCategoryResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest);

    }
}
