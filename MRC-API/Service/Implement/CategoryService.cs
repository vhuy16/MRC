using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response.Category;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;

namespace MRC_API.Service.Implement
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Category> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewCategoryResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest)
        {
            var categoryExist = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.CategoryName.Equals(createNewCategoryRequest.CategoryName) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if(categoryExist != null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryExisted);
            }
            Category category = new Category()
            {
                Id = Guid.NewGuid(),
                CategoryName = createNewCategoryRequest.CategoryName,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum()
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

        public async Task<bool> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (category == null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
            }
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync(
                predicate: p => p.CategoryId.Equals(category.Id) && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            foreach(var product in products)
            {
                var images = await _unitOfWork.GetRepository<Image>().GetListAsync(
                    predicate: i => i.ProductId.Equals(product.Id));
                if (images != null)
                {
                    foreach(var image in images)
                    {
                        _unitOfWork.GetRepository<Image>().DeleteAsync(image);
                    }
                }
                product.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
                product.UpDate = TimeUtils.GetCurrentSEATime();
                _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }
            category.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            category.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<IPaginate<GetCategoryResponse>> GetAllCategory(int page, int size)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetPagingListAsync(
                selector: c => new GetCategoryResponse() 
                { 
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName,
                },
                predicate: c => c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                size: size,
                page: page);
            return categories;
        }

        public async Task<GetCategoryResponse> GetCategory(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                selector: c => new GetCategoryResponse()
                {
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName,
                },
                predicate: c => c.Id.Equals(id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if(category == null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
            }
            return category;
        }

        public async Task<bool> UpdateCategory(Guid id, UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (category == null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
            }
            category.CategoryName = string.IsNullOrEmpty(updateCategoryRequest.CategoryName) ? category.CategoryName : updateCategoryRequest.CategoryName;
            category.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Category>().UpdateAsync(category);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }
    }
}
