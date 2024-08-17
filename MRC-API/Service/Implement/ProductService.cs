using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System.Net.Http.Headers;
using System.Text.Json;

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
