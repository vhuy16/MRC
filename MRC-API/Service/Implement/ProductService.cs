using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
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
using System.Drawing;
using System.Net.Http.Headers;
using System.Text.Json;
using Image = Repository.Entity.Image;



namespace MRC_API.Service.Implement
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private const string FirebaseStorageBaseUrl = "https://firebasestorage.googleapis.com/v0/b/mrc-firebase-d6e85.appspot.com/o";
        public ProductService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Product> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<ApiResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            // Check category ID
            var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(createProductRequest.CategoryId));
            if (cateCheck == null)
            {
                return new ApiResponse { status = "error", message = MessageConstant.CategoryMessage.CategoryNotExist, data = null };
            }

            // Check product name
            var prodCheck = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.ProductName.Equals(createProductRequest.ProductName));
            if (prodCheck != null)
            {
                return new ApiResponse { status = "error", message = MessageConstant.ProductMessage.ProductNameExisted, data = null };
            }

            // Validate quantity
            if (createProductRequest.Quantity < 0)
            {
                return new ApiResponse { status = "error", message = MessageConstant.ProductMessage.NegativeQuantity, data = null };
            }

            Product product = new Product
            {
                Id = Guid.NewGuid(),
                ProductName = createProductRequest.ProductName,
                CategoryId = createProductRequest.CategoryId,
                Description = createProductRequest.Description,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Price = createProductRequest.Price,
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

            await _unitOfWork.GetRepository<Product>().InsertAsync(product);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessful)
            {
                var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(createProductRequest.CategoryId));
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
                        CategoryName = category.CategoryName,
                    }
                };
            }

            return new ApiResponse { status = "error", message = "Failed to create product.", data = null };
        }

        public async Task<ApiResponse> GetListProduct(int page, int size)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetPagingListAsync(
                selector: s => new GetProductResponse
                {
                    Id = s.Id,
                    CategoryName = s.Category.CategoryName,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                },
                predicate: p => p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                page: page,
                size: size
            );

            if (products == null || products.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "No products found.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Products retrieved successfully.",
                data = products
            };
        }

        public async Task<ApiResponse> GetListProductByCategoryId(Guid CateID, int page, int size)
        {
            // Check if the category exists
            var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(CateID)
            );

            if (cateCheck == null)
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
                    CategoryName = s.Category.CategoryName,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                },
                predicate: p => p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()) && p.CategoryId.Equals(CateID),
                page: page,
                size: size
            );

            if (products == null || products.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "No products found in this category.",
                    data = null
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
                    CategoryName = s.Category.CategoryName,
                    Description = s.Description,
                    Images = s.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = s.ProductName,
                    Quantity = s.Quantity,
                },
                predicate: p => p.Id.Equals(productId));

            if (product == null)
            {
                return new ApiResponse { status = StatusCodes.Status404NotFound.ToString(), message = MessageConstant.ProductMessage.ProductNotExist, data = null };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Product retrieved successfully.",
                data = product
            };
        }

        //public async Task<bool> UpdateProduct(Guid ProID, UpdateProductRequest updateProductRequest)
        //{
        //    var productUpdate = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
        //        predicate: p => p.Id.Equals(ProID)
        //        );
        //    if (productUpdate == null)
        //    {
        //        throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
        //    }
        //    productUpdate.ProductName = string.IsNullOrEmpty(updateProductRequest.ProductName) ? productUpdate.ProductName : updateProductRequest.ProductName;
        //    productUpdate.Description = string.IsNullOrEmpty(updateProductRequest.Description) ? productUpdate.Description : updateProductRequest.Description;
        //    if (updateProductRequest.CategoryId.HasValue)
        //    {
        //        productUpdate.CategoryId = updateProductRequest.CategoryId.Value;
        //    }
        //    if (updateProductRequest.Quantity.HasValue)
        //    {
        //        productUpdate.Quantity = updateProductRequest.Quantity.Value;
        //    }
        //    if (updateProductRequest.ImageLink != null && updateProductRequest.ImageLink.Any())
        //    {
        //        foreach (var image in productUpdate.Images)
        //        {
        //            _unitOfWork.GetRepository<Image>().DeleteAsync(image);
        //        }

        //        // Upload new images
        //        var imageUrls = await UploadFilesToFirebase(updateProductRequest.ImageLink);
        //        foreach (var imageUrl in imageUrls)
        //        {
        //            productUpdate.Images.Add(new Image {
        //                Id = Guid.NewGuid(),
        //                ProductId = productUpdate.Id,
        //                InsDate = TimeUtils.GetCurrentSEATime(),
        //                UpDate = TimeUtils.GetCurrentSEATime(),
        //                LinkImage = imageUrl
        //                 });
        //        }
        //    }
        //    productUpdate.UpDate = TimeUtils.GetCurrentSEATime();
        //    _unitOfWork.GetRepository<Product>().UpdateAsync(productUpdate);
        //    bool IsSuccessful = await _unitOfWork.CommitAsync() > 0;
        //    return IsSuccessful;
        //}
        public async Task<ApiResponse> UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest)
        {
            // Check if the product exists
            var existingProduct = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(productId));
            if (existingProduct == null)
            {
                return new ApiResponse { status = StatusCodes.Status404NotFound.ToString(), message = MessageConstant.ProductMessage.ProductNotExist, data = null };
            }

            // Check CategoryId if provided
            if (updateProductRequest.CategoryId.HasValue)
            {
                var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(updateProductRequest.CategoryId.Value));
                if (cateCheck == null)
                {
                    return new ApiResponse { status = StatusCodes.Status400BadRequest.ToString(), message = MessageConstant.CategoryMessage.CategoryNotExist, data = null };
                }
                existingProduct.CategoryId = updateProductRequest.CategoryId.Value;
            }

            // Check product name if provided
            if (!string.IsNullOrEmpty(updateProductRequest.ProductName) && !existingProduct.ProductName.Equals(updateProductRequest.ProductName))
            {
                var prodCheck = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.ProductName.Equals(updateProductRequest.ProductName));
                if (prodCheck != null)
                {
                    return new ApiResponse { status = StatusCodes.Status400BadRequest.ToString(), message = MessageConstant.ProductMessage.ProductNameExisted, data = null };
                }
                existingProduct.ProductName = updateProductRequest.ProductName;
            }

            // Check quantity if provided
            if (updateProductRequest.Quantity.HasValue)
            {
                if (updateProductRequest.Quantity < 0)
                {
                    return new ApiResponse { status = StatusCodes.Status400BadRequest.ToString(), message = MessageConstant.ProductMessage.NegativeQuantity, data = null };
                }
                existingProduct.Quantity = updateProductRequest.Quantity.Value;
            }

            // Update description if provided
            if (!string.IsNullOrEmpty(updateProductRequest.Description))
            {
                existingProduct.Description = updateProductRequest.Description;
            }

            // Update images if provided
            if (updateProductRequest.ImageLink != null && updateProductRequest.ImageLink.Any())
            {
                var existingImages = await _unitOfWork.GetRepository<Image>().GetListAsync(predicate: i => i.ProductId.Equals(existingProduct.Id));
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
                var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(existingProduct.CategoryId));
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
                        CategoryName = category.CategoryName,
                    }
                };
            }

            return new ApiResponse { status = StatusCodes.Status500InternalServerError.ToString(), message = "Failed to update product.", data = null };
        }
        public async Task<ApiResponse> DeleteProduct(Guid productId)
        {
            if (productId == Guid.Empty)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductIdEmpty);
            }

            // Find product
            var existingProduct = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(productId));
            if (existingProduct == null)
            {
                return new ApiResponse { status = StatusCodes.Status404NotFound.ToString(), message = MessageConstant.ProductMessage.ProductNotExist, data = null };
            }

            // Mark as deleted
            existingProduct.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            _unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (isSuccessful)
            {
                return new ApiResponse { status = StatusCodes.Status204NoContent.ToString(), message = "Product deleted successfully.", data = null };
            }

            return new ApiResponse { status = StatusCodes.Status500InternalServerError.ToString(), message = "Failed to delete product.", data = null };
        }
        private async Task<List<string>> UploadFilesToFirebase(List<IFormFile> formFiles)
        {
            var uploadedUrls = new List<string>();

            try
            {
                using (var client = new HttpClient())
                {
                    foreach (var formFile in formFiles)
                    {
                        if (formFile.Length > 0)
                        {
                            string fileName = Path.GetFileName(formFile.FileName);
                            string firebaseStorageUrl = $"{FirebaseStorageBaseUrl}?uploadType=media&name=images/{Guid.NewGuid()}_{fileName}";

                            using (var stream = new MemoryStream())
                            {
                                await formFile.CopyToAsync(stream);
                                stream.Position = 0;
                                var content = new ByteArrayContent(stream.ToArray());
                                content.Headers.ContentType = new MediaTypeHeaderValue(formFile.ContentType);

                                var response = await client.PostAsync(firebaseStorageUrl, content);
                                if (response.IsSuccessStatusCode)
                                {
                                    var responseBody = await response.Content.ReadAsStringAsync();
                                    var downloadUrl = ParseDownloadUrl(responseBody, fileName);
                                    uploadedUrls.Add(downloadUrl);
                                }
                                else
                                {
                                    var errorMessage = $"Error uploading file {fileName} to Firebase Storage. Status Code: {response.StatusCode}\nContent: {await response.Content.ReadAsStringAsync()}";

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return uploadedUrls;
        }

        private string ParseDownloadUrl(string responseBody, string fileName)
        {
            // This assumes the response contains a JSON object with the field "name" which is the path to the uploaded file.
            var json = JsonDocument.Parse(responseBody);
            var nameElement = json.RootElement.GetProperty("name");
            var downloadUrl = $"{FirebaseStorageBaseUrl}/{Uri.EscapeDataString(nameElement.GetString())}?alt=media";
            return downloadUrl;
        }
    }
}
