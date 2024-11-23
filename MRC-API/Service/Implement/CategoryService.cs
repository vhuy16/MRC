using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Category;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Category;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MRC_API.Service.Implement
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        public CategoryService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Category> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateNewCategory(CreateNewCategoryRequest createNewCategoryRequest)
        {
            var categoryExist = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.CategoryName.Equals(createNewCategoryRequest.CategoryName) &&
                                c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (categoryExist != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryExisted,
                    data = null
                };
            }

            Category category = new Category
            {
                Id = Guid.NewGuid(),
                CategoryName = createNewCategoryRequest.CategoryName,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Category>().InsertAsync(category);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to create category.",
                    data = null
                };
            }

            var response = new CreateNewCategoryResponse
            {
                CategoryId = category.Id,
                CategoryName = category.CategoryName,
            };

            return new ApiResponse
            {
                status = StatusCodes.Status201Created.ToString(),
                message = "Category created successfully.",
                data = response
            };
        }

        public async Task<ApiResponse> DeleteCategory(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(id) &&
                                c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (category == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            var products = await _unitOfWork.GetRepository<Product>().GetListAsync(
                predicate: p => p.CategoryId.Equals(category.Id) &&
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

            category.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            category.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Category>().UpdateAsync(category);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to delete category.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Category deleted successfully.",
                data = true
            };
        }

        public async Task<ApiResponse> GetAllCategory(int page, int size, string? searchName, bool? isAscending)
        {
            var categories = await _unitOfWork.GetRepository<Category>().GetPagingListAsync(
                selector: c => new GetCategoryResponse
                {
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName,
                },
                predicate: p => p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()) &&
                                (string.IsNullOrEmpty(searchName) || p.CategoryName.Contains(searchName)),
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.CategoryName) : q.OrderByDescending(p => p.CategoryName))
                    : q.OrderByDescending(p => p.InsDate),
            size: size,
                page: page);
            int totalItems = categories.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (categories == null || categories.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Category retrieved successfully.",
                    data = new Paginate<Category>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Category>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Categories retrieved successfully.",
                data = categories
            };
        }

        public async Task<ApiResponse> GetCategory(Guid id)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                selector: c => new GetCategoryResponse
                {
                    CategoryId = c.Id,
                    CategoryName = c.CategoryName,
                },
                predicate: c => c.Id.Equals(id) &&
                                c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (category == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Category retrieved successfully.",
                data = category
            };
        }

        public async Task<ApiResponse> UpdateCategory(Guid id, UpdateCategoryRequest updateCategoryRequest)
        {
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(id) &&
                                c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (category == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            category.CategoryName = string.IsNullOrEmpty(updateCategoryRequest.CategoryName)
                ? category.CategoryName
                : updateCategoryRequest.CategoryName;
            category.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Category>().UpdateAsync(category);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to update category.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Category updated successfully.",
                data = true
            };
        }
    }
}
