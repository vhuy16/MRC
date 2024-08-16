using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;

namespace MRC_API.Service.Implement
{
    public class ProductService : BaseService<Product>, IProductService
    {
        public ProductService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Product> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }
        public async Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                ProductName = createProductRequest.ProductName,
                CategoryId = createProductRequest.CategoryId,
                Description = createProductRequest.Description,
                //Images = createProductRequest.ImageLink
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Quantity = createProductRequest.Quantity,
                Status = StatusEnum.Available.GetDescriptionFromEnum(),
                
            };

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
                    Images = product.Images,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    CategoryName = category.CategoryName,
                };

            }
            return createProductResponse;
        }
    }
}
