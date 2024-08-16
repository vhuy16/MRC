using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;
using MRC_API.Service.Interface;
using Repository.Entity;

namespace MRC_API.Service.Implement
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Category> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewCategoryResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest)
        {
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = createNewCategoryRequest.CategoryName,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
            };

            await _unitOfWork.GetRepository<Category>().InsertAsync(category);
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewCategoryResponse createNewCategoryResponse = null;
            if (isSuccesfully)
            {
                createNewCategoryResponse = new CreateNewCategoryResponse()
                {
                    CategoryId = category.Id,
                    CategoryName = category.CategoryName,
                };
            }
            return createNewCategoryResponse;
        }
    }
}
