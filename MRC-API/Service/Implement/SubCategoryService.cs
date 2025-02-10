using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.SubCategory;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.SubCategory;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;

namespace MRC_API.Service.Implement
{
    public class SubCategoryService : BaseService<SubCategoryService>, ISubCategoryService
    {
        public SubCategoryService(IUnitOfWork<MrcContext> unitOfWork, ILogger<SubCategoryService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateSubCategory(CreateSubCategoryRequest createSubCategoryRequest)
        {
            if(createSubCategoryRequest.CategoryId == null || string.IsNullOrEmpty(createSubCategoryRequest.SubCategoryName))
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.SubCategoryMessage.CreateSubCategoryFail,
                    data = null
                };
            }

            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(createSubCategoryRequest.CategoryId) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if(category == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            var subCategoryExist = await _unitOfWork.GetRepository<SubCategory>().SingleOrDefaultAsync(
                predicate: s => s.SubCategoryName.Equals(createSubCategoryRequest.SubCategoryName) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (subCategoryExist != null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.SubCategoryMessage.SubCategoryExist,
                    data = null
                };
            }

            SubCategory subCategory = new SubCategory()
            {
                Id = Guid.NewGuid(),
                SubCategoryName = createSubCategoryRequest.SubCategoryName,
                CategoryId = createSubCategoryRequest.CategoryId,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
            };

            await _unitOfWork.GetRepository<SubCategory>().InsertAsync(subCategory);
            await _unitOfWork.CommitAsync();

            return new ApiResponse()
            {
                status = StatusCodes.Status201Created.ToString(),
                message = MessageConstant.SubCategoryMessage.CreateSubCategorySuccessfully,
                data = new CreateSubCategoryResponse()
                {
                    CategoryId = subCategory.Id,
                    SubCategoryName = subCategory.SubCategoryName
                }
            };
        }

        public async Task<ApiResponse> DeleteSubCategory(Guid id)
        {
            var subCategory = await _unitOfWork.GetRepository<SubCategory>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (subCategory == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.SubCategoryMessage.SubCategoryNotExist,
                    data = false
                };
            }
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync(
                predicate: p => p.SubCategoryId.Equals(subCategory.Id) &&
                                p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            foreach (var product in products)
            {
                var images = await _unitOfWork.GetRepository<Image>().GetListAsync(
                    predicate: i => i.ProductId.Equals(product.Id));

                foreach (var image in images)
                {
                    _unitOfWork.GetRepository<Image>().DeleteAsync(image);
                }

                var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                    predicate: ci => ci.ProductId.Equals(product.Id));

                foreach (var cartItem in cartItems)
                {
                    _unitOfWork.GetRepository<CartItem>().DeleteAsync(cartItem);
                }

                product.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
                product.UpDate = TimeUtils.GetCurrentSEATime();
                _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }

            subCategory.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            subCategory.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<SubCategory>().UpdateAsync(subCategory);
            await _unitOfWork.CommitAsync();
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Xóa thành công",
                data = true
            };
        }

        public async Task<ApiResponse> GetListSubCategoryByCategoryId(Guid id, int page, int size)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if(category == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            var subCategories = await _unitOfWork.GetRepository<SubCategory>().GetPagingListAsync(
                selector: s => new GetsubCategoryResponse()
                {
                    SubCategoryId = s.Id,
                    SubCategoryName = s.SubCategoryName
                },
                predicate: s => s.CategoryId.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                page: page,
                size: size);

            int totalItems = subCategories.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);

            if (subCategories == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Danh sách danh mục phụ",
                    data = new Paginate<SubCategory>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<SubCategory>()
                    }
                };
            }


            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Danh sách danh mục phụ",
                data = subCategories
            };
        }

        public async Task<ApiResponse> GetSubCategories(int page, int size)
        {
            var subCategories = await _unitOfWork.GetRepository<SubCategory>().GetPagingListAsync(
                selector: s => new GetsubCategoryResponse()
                {
                    SubCategoryId = s.Id,
                    SubCategoryName = s.SubCategoryName
                },
                //include: s => s.Include(s => s.Products),
                predicate: s => s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
            page: page,
                size: size);

            int totalItems = subCategories.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);

            if (subCategories == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Danh sách danh mục phụ",
                    data = new Paginate<SubCategory>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<SubCategory>()
                    }
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Danh sách danh mục phụ",
                data = subCategories
            };
        }

        public async Task<ApiResponse> GetSubCategory(Guid id)
        {
            var subCategory = await _unitOfWork.GetRepository<SubCategory>().SingleOrDefaultAsync(
                selector: s => new GetsubCategoryResponse()
                {
                    SubCategoryName = s.SubCategoryName,
                    SubCategoryId = s.CategoryId
                },
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            
            if (subCategory == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.SubCategoryMessage.SubCategoryNotExist,
                    data = null
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Danh mục phụ",
                data = subCategory
            };

        }

        public async Task<ApiResponse> UpdateSubCategory(Guid id, UpdateSubCategoryRequest updateSubCategoryRequest)
        {
            var existingSubCategory = await _unitOfWork.GetRepository<SubCategory>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (existingSubCategory == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.SubCategoryMessage.SubCategoryNotExist,
                    data = null
                };
            }

            existingSubCategory.SubCategoryName = string.IsNullOrEmpty(updateSubCategoryRequest.SubCategoryName)
                ? existingSubCategory.SubCategoryName
                : updateSubCategoryRequest.SubCategoryName;


            _unitOfWork.GetRepository<SubCategory>().UpdateAsync(existingSubCategory);
            await _unitOfWork.CommitAsync();
            
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Update thành công",
                data = existingSubCategory
            };
        }
    }
}
