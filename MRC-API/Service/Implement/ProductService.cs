using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.Product;
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
        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            // kiểm tra categoryID
            // kiểm tra tên product
            // quantity không đc là số âm
            var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(createProductRequest.CategoryId));
            if (cateCheck == null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
            }
            var prodCheck = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.ProductName.Equals(createProductRequest.ProductName));
            if (prodCheck != null)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNameExisted);
            }
            if(createProductRequest.Quantity < 0)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.NegativeQuantity);
            }
            Product product = new Product()
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
            CreateProductResponse createProductResponse = null;
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(predicate: c => c.Id.Equals(createProductRequest.CategoryId));
            if (isSuccessful)
            {
                createProductResponse = new CreateProductResponse()
                {
                    Id = product.Id,
                    Description = product.Description,
                    Images = product.Images.Select(i => i.LinkImage).ToList(),
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CategoryName = category.CategoryName,
                };

            }
            return createProductResponse;
        }
        public async Task<IPaginate<GetProductResponse>> GetListProduct(int page, int size)
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
                page : page,
                size : size
                );
            return products;
        }
        public async Task<IPaginate<GetProductResponse>> GetListProductByCategoryId (Guid CateID, int page, int size)
        {
            var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                 predicate: c => c.Id.Equals(CateID));
            if (cateCheck == null)
            {
                throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
            }
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
            return products;
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
        public async Task<UpdateProductResponse> UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest)
        {
            // Kiểm tra sự tồn tại của sản phẩm
            var existingProduct = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(productId));
            if (existingProduct == null)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
            }

            // Kiểm tra CategoryId nếu nó được cung cấp
            if (updateProductRequest.CategoryId.HasValue)
            {
                var cateCheck = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                    predicate: c => c.Id.Equals(updateProductRequest.CategoryId.Value));
                if (cateCheck == null)
                {
                    throw new BadHttpRequestException(MessageConstant.CategoryMessage.CategoryNotExist);
                }
                existingProduct.CategoryId = updateProductRequest.CategoryId.Value;
            }

            // Kiểm tra tên sản phẩm nếu nó được cung cấp
            if (!string.IsNullOrEmpty(updateProductRequest.ProductName) && !existingProduct.ProductName.Equals(updateProductRequest.ProductName))
            {
                var prodCheck = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                    predicate: p => p.ProductName.Equals(updateProductRequest.ProductName));
                if (prodCheck != null)
                {
                    throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNameExisted);
                }
                existingProduct.ProductName = updateProductRequest.ProductName;
            }

            // Kiểm tra số lượng nếu nó được cung cấp và không được là số âm
            if (updateProductRequest.Quantity.HasValue)
            {
                if (updateProductRequest.Quantity < 0)
                {
                    throw new BadHttpRequestException(MessageConstant.ProductMessage.NegativeQuantity);
                }
                existingProduct.Quantity = updateProductRequest.Quantity.Value;
            }

            // Cập nhật mô tả nếu nó được cung cấp
            if (!string.IsNullOrEmpty(updateProductRequest.Description))
            {
                existingProduct.Description = updateProductRequest.Description;
            }

            // Cập nhật ngày sửa đổi
            existingProduct.UpDate = TimeUtils.GetCurrentSEATime();

            // Cập nhật hình ảnh nếu nó được cung cấp
            if (updateProductRequest.ImageLink != null && updateProductRequest.ImageLink.Any())
            {
                // Clear existing images from the database
                var existingImages = await _unitOfWork.GetRepository<Image>().GetListAsync(predicate: i => i.ProductId.Equals(existingProduct.Id));
                foreach (var img in existingImages)
                {
                    _unitOfWork.GetRepository<Image>().DeleteAsync(img);
                }

                // Upload new images
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

            // Step 4: Commit the changes to the database
            _unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);
            bool isSuccessful = false;

            try
            {
                isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception("Failed to save product and images to the database.");
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Product)
                    {
                        var clientValues = (Product)entry.Entity;
                        var databaseEntry = entry.GetDatabaseValues();

                        if (databaseEntry == null)
                        {
                            throw new Exception("Product was deleted by another user.");
                        }
                        else
                        {
                            var databaseValues = (Product)databaseEntry.ToObject();
                            throw new Exception("The product you attempted to update was modified by another user.");
                        }
                    }
                }
            }

            // Step 5: Manually load images after commit
            var imagesAfterCommit = await _unitOfWork.GetRepository<Image>().GetListAsync(predicate: i => i.ProductId.Equals(existingProduct.Id));
            existingProduct.Images = imagesAfterCommit.ToList();

            if (existingProduct.Images == null || !existingProduct.Images.Any())
            {
                throw new Exception("No images found in the database after commit.");
            }

            // Step 6: Prepare and return the response
            var category = await _unitOfWork.GetRepository<Category>().SingleOrDefaultAsync(
                predicate: c => c.Id.Equals(existingProduct.CategoryId));

            return new UpdateProductResponse()
            {
                Id = existingProduct.Id,
                Description = existingProduct.Description,
                Images = existingProduct.Images.Select(i => i.LinkImage).ToList(),
                ProductName = existingProduct.ProductName,
                Quantity = existingProduct.Quantity,
                CategoryName = category.CategoryName,
            };
        }
        public async Task<bool> DeleteProduct(Guid ProductId)
        {
            if(ProductId == null)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductIdEmpty);

            }
            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(ProductId) 
                && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if(product == null)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductIsEmpty);
            }
            product.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();

            var imageDelete = await _unitOfWork.GetRepository<Image>().GetListAsync(predicate: p => p.ProductId.Equals(product.Id));
            if(imageDelete != null)
            {
                foreach(var img in imageDelete)
                {
                    _unitOfWork.GetRepository<Image>().DeleteAsync(img);
                }
            }

            var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                predicate: ci => ci.ProductId.Equals(ProductId));
            if(cartItems != null) 
            {
                foreach (var cartItem in cartItems)
                {
                    _unitOfWork.GetRepository<CartItem>().DeleteAsync(cartItem);
                }
            }
            _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            bool isSuccessful = await _unitOfWork.CommitAsync() >0;
            return isSuccessful;
               


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
