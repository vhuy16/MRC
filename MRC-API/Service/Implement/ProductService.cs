using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Ganss.Xss;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Product;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System;
using System.Drawing;
using System.Net.Http.Headers;
using System.Text.Json;
using Image = Repository.Entity.Image;

namespace MRC_API.Service.Implement
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private const string FirebaseStorageBaseUrl =
            "https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o";

        private readonly HtmlSanitizerUtils _sanitizer;

        public ProductService(
            IUnitOfWork<MrcContext> unitOfWork,
            ILogger<Product> logger,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            HtmlSanitizerUtils htmlSanitizer
        ) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _sanitizer = htmlSanitizer;
        }

        public async Task<ApiResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            // Check category ID
            var subCateCheck = await _unitOfWork.GetRepository<SubCategory>()
                .SingleOrDefaultAsync(predicate: c => c.Id.Equals(createProductRequest.SubCategoryId));
            if (subCateCheck == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist, data = null
                };
            }

            // Check product name
            var prodCheck = await _unitOfWork.GetRepository<Product>()
                .SingleOrDefaultAsync(predicate: p => p.ProductName.Equals(createProductRequest.ProductName));
            if (prodCheck != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.ProductMessage.ProductNameExisted, data = null
                };
            }

            // Validate quantity
            if (createProductRequest.Quantity < 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.ProductMessage.NegativeQuantity, data = null
                };
            }

            var validationResult = ValidateImages(createProductRequest.ImageLink);
            if (validationResult.Any())
            {
                return new ApiResponse()
                {
                    status = "400",
                    listErrorMessage = validationResult,
                    data = null
                };
            }

            createProductRequest.Description = _sanitizer.Sanitize(createProductRequest.Description);
            createProductRequest.Message = _sanitizer.Sanitize(createProductRequest.Message);
            Product product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = createProductRequest.ProductName,
                SubCategoryId = createProductRequest.SubCategoryId,
                Description = createProductRequest.Description,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Price = createProductRequest.Price,
                Message = createProductRequest.Message,
                Quantity = createProductRequest.Quantity,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                Images = new List<Image>()
            };

            if (createProductRequest.ImageLink != null && createProductRequest.ImageLink.Any())
            {
                var imageUrls = await UploadFilesToFirebase(createProductRequest.ImageLink);
                foreach (var imageUrl in imageUrls)
                {
                    product.Images.Add(new Image
                    {
                        Id = Guid.NewGuid(),
                        ProductId = product.Id,
                        InsDate = TimeUtils.GetCurrentSEATime(),
                        UpDate = TimeUtils.GetCurrentSEATime(),
                        LinkImage = imageUrl
                    });
                }
            }

            try
            {
                await _unitOfWork.GetRepository<Product>().InsertAsync(product);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (isSuccessful)
                {
                    var subCategory = await _unitOfWork.GetRepository<SubCategory>()
                        .SingleOrDefaultAsync(predicate: c => c.Id.Equals(createProductRequest.SubCategoryId));
                    return new ApiResponse
                    {
                        status = StatusCodes.Status201Created.ToString(),
                        message = "Product created successfully.",
                        data = new CreateProductResponse
                        {
                            Id = product.Id,
                            Description = product.Description,
                            Images = product.Images.Select(i => i.LinkImage).ToList(),
                            ProductName = product.ProductName,
                            Quantity = product.Quantity,
                            Message = product.Message,
                            SubCategoryName = subCategory.SubCategoryName,
                            Price = product.Price,
                        }
                    };
                }
                else
                {
                    return new ApiResponse { status = "error", message = "Failed to create product.", data = null };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = "error",
                    message = $"An error occurred: {ex.Message}",
                    data = null
                };
            }
        }

        public async Task<ApiResponse> GetAllProduct(int page, int size, string status, string? searchName,
            bool? isAscending, string? subCategoryName)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetPagingListAsync(
                selector: s => new GetProductResponse
                {
                    Id = s.Id,
                    SubCategoryName = s.SubCategory.SubCategoryName,
                    CategoryId = s.SubCategory.CategoryId,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    Message = s.Message,
                    Price = s.Price,
                    SubCategoryId = s.SubCategoryId,
                    Status = s.Status,
                    CategoryName = s.SubCategory.Category.CategoryName
                },
                include: i => i.Include(p => p.SubCategory),
                predicate: p =>
                    (string.IsNullOrEmpty(searchName) || p.ProductName.Contains(searchName)) && // Filter by name
                    (string.IsNullOrEmpty(status) || p.Status.Equals(status)) && // Filter by status
                    (string.IsNullOrEmpty(subCategoryName) ||
                     p.SubCategory.SubCategoryName.Contains(subCategoryName)), // Filter by subcategory
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.Price) : q.OrderByDescending(p => p.Price))
                    : q.OrderByDescending(p => p.InsDate),
                page: page,
                size: size
            );

            int totalItems = products.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (products == null || products.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Products retrieved successfully.",
                    data = new Paginate<Product>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Product>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Products retrieved successfully.",
                data = products
            };
        }

        public async Task<ApiResponse> GetListProduct(int page, int size, string? search, bool? isAscending,
            string? subCategoryName, decimal? minPrice, decimal? maxPrice)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetPagingListAsync(
                selector: s => new GetProductResponse
                {
                    Id = s.Id,
                    SubCategoryName = s.SubCategory.SubCategoryName,
                    Description = s.Description,
                    SubCategoryId = s.SubCategoryId,
                    CategoryId = s.SubCategory.CategoryId,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Message = s.Message,
                    Quantity = s.Quantity,
                    Price = s.Price,
                    Status = s.Status,
                    CategoryName = s.SubCategory.Category.CategoryName
                },
                predicate: p =>
                    p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()) &&
                    (string.IsNullOrEmpty(search) ||
                     p.ProductName.ToLower().Contains(search.ToLower()) ||
                     p.Description.ToLower().Contains(search.ToLower()) ||
                     (!string.IsNullOrEmpty(p.Message) && p.Message.ToLower().Contains(search.ToLower())))
                    && // Tìm kiếm toàn diện
                    (string.IsNullOrEmpty(subCategoryName) ||
                     p.SubCategory.SubCategoryName.Equals(subCategoryName)) && // Filter theo category
                    (!minPrice.HasValue || p.Price >= minPrice.Value) && // Filter giá tối thiểu
                    (!maxPrice.HasValue || p.Price <= maxPrice.Value), // Filter giá tối đa
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.Price) : q.OrderByDescending(p => p.Price))
                    : q.OrderByDescending(p => p.InsDate),
                page: page,
                size: size
            );

            int totalItems = products.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (products == null || products.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "No products found.",
                    data = new Paginate<Product>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Product>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Products retrieved successfully.",
                data = products
            };
        }

        public async Task<ApiResponse> GetListProductByCategoryId(Guid subCateId, int page, int size)
        {
            // Check if the category exists
            var subCateCheck = await _unitOfWork.GetRepository<SubCategory>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(subCateId)
            );

            if (subCateCheck == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CategoryMessage.CategoryNotExist,
                    data = null
                };
            }

            // Retrieve a paginated list of products by category ID
            var products = await _unitOfWork.GetRepository<Product>().GetPagingListAsync(
                selector: s => new GetProductResponse
                {
                    Id = s.Id,
                    SubCategoryName = s.SubCategory.SubCategoryName,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    Message = s.Message,
                    Price = s.Price,
                    Status = s.Status
                },
                predicate: p =>
                    p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()) && p.SubCategoryId.Equals(subCateId),
                page: page,
                size: size
            );

            int totalItems = products.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (products == null || products.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Products retrieved successfully.",
                    data = new Paginate<Product>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Product>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Products retrieved successfully.",
                data = products
            };
        }

        public async Task<ApiResponse> GetProductById(Guid productId)
        {
            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                selector: s => new GetProductResponse
                {
                    Id = s.Id,
                    SubCategoryId = s.SubCategoryId,
                    SubCategoryName = s.SubCategory.SubCategoryName,
                    CategoryId = s.SubCategory.CategoryId,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                    Message = s.Message,
                    Status = s.Status,
                    Price = s.Price,
                    CategoryName = s.SubCategory.Category.CategoryName,
                },
                predicate: p => p.Id.Equals(productId));

            if (product == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ProductMessage.ProductNotExist, data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Product retrieved successfully.",
                data = product
            };
        }

        public async Task<ApiResponse> UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest)
        {
            // Check if the product exists
            var existingProduct = await _unitOfWork.GetRepository<Product>()
                .SingleOrDefaultAsync(predicate: p => p.Id.Equals(productId));
            if (existingProduct == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ProductMessage.ProductNotExist, data = null
                };
            }

            // Check CategoryId if provided
            if (updateProductRequest.SubCategoryId.HasValue)
            {
                var subCateCheck = await _unitOfWork.GetRepository<SubCategory>()
                    .SingleOrDefaultAsync(predicate: c => c.Id.Equals(updateProductRequest.SubCategoryId.Value));
                if (subCateCheck == null)
                {
                    return new ApiResponse
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = MessageConstant.CategoryMessage.CategoryNotExist, data = null
                    };
                }

                existingProduct.SubCategoryId = updateProductRequest.SubCategoryId.Value;
            }

            // Check product name if provided
            if (!string.IsNullOrEmpty(updateProductRequest.ProductName) &&
                !existingProduct.ProductName.Equals(updateProductRequest.ProductName))
            {
                var prodCheck = await _unitOfWork.GetRepository<Product>()
                    .SingleOrDefaultAsync(predicate: p => p.ProductName.Equals(updateProductRequest.ProductName));
                if (prodCheck != null)
                {
                    return new ApiResponse
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = MessageConstant.ProductMessage.ProductNameExisted, data = null
                    };
                }

                existingProduct.ProductName = updateProductRequest.ProductName;
            }

            if (!string.IsNullOrEmpty(updateProductRequest.Status) &&
                !existingProduct.Status.Equals(updateProductRequest.Status))
            {
                existingProduct.Status = updateProductRequest.Status;
            }

            if (updateProductRequest.Price.HasValue)
            {
                if (updateProductRequest.Price <= 0)
                {
                    return new ApiResponse
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = MessageConstant.ProductMessage.NegativeQuantity, data = null
                    };
                }

                existingProduct.Price = updateProductRequest.Price.Value;
            }

            // Check quantity if provided
            if (updateProductRequest.Quantity.HasValue)
            {
                if (updateProductRequest.Quantity < 0)
                {
                    return new ApiResponse
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = MessageConstant.ProductMessage.NegativeQuantity, data = null
                    };
                }

                existingProduct.Quantity = updateProductRequest.Quantity.Value;
            }

            if (!string.IsNullOrEmpty(updateProductRequest.Message))
            {
                existingProduct.Message = updateProductRequest.Message;
            }

            // Update description if provided
            if (!string.IsNullOrEmpty(updateProductRequest.Description))
            {
                existingProduct.Description = _sanitizer.Sanitize(updateProductRequest.Description);
            }

            // Update images if provided
            if (updateProductRequest.ImageLink != null && updateProductRequest.ImageLink.Any())
            {
                var existingImages = await _unitOfWork.GetRepository<Image>()
                    .GetListAsync(predicate: i => i.ProductId.Equals(existingProduct.Id));
                foreach (var img in existingImages)
                {
                    _unitOfWork.GetRepository<Image>().DeleteAsync(img);
                }

                var imageUrls = await UploadFilesToFirebase(updateProductRequest.ImageLink);
                foreach (var imageUrl in imageUrls)
                {
                    var newImage = new Image
                    {
                        Id = Guid.NewGuid(),
                        ProductId = existingProduct.Id,
                        InsDate = TimeUtils.GetCurrentSEATime(),
                        UpDate = TimeUtils.GetCurrentSEATime(),
                        LinkImage = imageUrl
                    };
                    existingProduct.Images.Add(newImage);
                    await _unitOfWork.GetRepository<Image>().InsertAsync(newImage);
                }
            }

            // Commit changes
            _unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessful)
            {
                var subCategory = await _unitOfWork.GetRepository<SubCategory>()
                    .SingleOrDefaultAsync(predicate: c => c.Id.Equals(existingProduct.SubCategoryId));
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Product updated successfully.",
                    data = new UpdateProductResponse
                    {
                        Id = existingProduct.Id,
                        Description = existingProduct.Description,
                        Images = existingProduct.Images.Select(i => i.LinkImage).ToList(),
                        ProductName = existingProduct.ProductName,
                        Quantity = existingProduct.Quantity,
                        Message = existingProduct.Message,
                        SubCategoryName = subCategory.SubCategoryName,
                        Price = existingProduct.Price
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status500InternalServerError.ToString(), message = "Failed to update product.",
                data = null
            };
        }

        public async Task<ApiResponse> PatchProductImages(Guid productId, List<IFormFile> newImages)
        {
            // Check if the product exists
            var existingProduct = await _unitOfWork.GetRepository<Product>()
                .SingleOrDefaultAsync(predicate: p => p.Id.Equals(productId), include: p => p.Include(i => i.Images));
            if (existingProduct == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ProductMessage.ProductNotExist,
                    data = null
                };
            }

            // Validate new images if provided
            if (newImages != null && newImages.Any())
            {
                var validationResult = ValidateImages(newImages);
                if (validationResult.Any())
                {
                    return new ApiResponse
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        listErrorMessage = validationResult,
                        data = null
                    };
                }
            }

            try
            {
                // Remove all existing images
                var existingImages = existingProduct.Images.ToList();
                foreach (var image in existingImages)
                {
                    existingProduct.Images.Remove(image);
                    _unitOfWork.GetRepository<Image>().DeleteAsync(image);
                }

                // Add new images if provided
                if (newImages != null && newImages.Any())
                {
                    var imageUrls = await UploadFilesToFirebase(newImages);
                    foreach (var imageUrl in imageUrls)
                    {
                        var newImage = new Image
                        {
                            Id = Guid.NewGuid(),
                            ProductId = existingProduct.Id,
                            InsDate = TimeUtils.GetCurrentSEATime(),
                            UpDate = TimeUtils.GetCurrentSEATime(),
                            LinkImage = imageUrl
                        };
                        existingProduct.Images.Add(newImage);
                        await _unitOfWork.GetRepository<Image>().InsertAsync(newImage);
                    }
                }

                // Update timestamp
                existingProduct.UpDate = TimeUtils.GetCurrentSEATime();

                // Commit changes
                _unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

                if (isSuccessful)
                {
                    var subCategory = await _unitOfWork.GetRepository<SubCategory>()
                        .SingleOrDefaultAsync(predicate: c => c.Id.Equals(existingProduct.SubCategoryId));
                    
                    return new ApiResponse
                    {
                        status = StatusCodes.Status200OK.ToString(),
                        message = "Product images updated successfully.",
                        data = new UpdateProductResponse
                        {
                            Id = existingProduct.Id,
                            Description = existingProduct.Description,
                            Images = existingProduct.Images.Select(i => i.LinkImage).ToList(),
                            ProductName = existingProduct.ProductName,
                            Quantity = existingProduct.Quantity,
                            Message = existingProduct.Message,
                            SubCategoryName = subCategory?.SubCategoryName,
                            Price = existingProduct.Price
                        }
                    };
                }

                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to update product images.",
                    data = null
                };
            }
            catch (Exception ex)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = $"An error occurred: {ex.Message}",
                    data = null
                };
            }
        }
