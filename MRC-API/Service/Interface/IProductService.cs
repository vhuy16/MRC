using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;

namespace MRC_API.Service.Interface
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest);
    }
}
