using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest);
        Task<IPaginate<GetProductResponse>> GetListProduct(int page, int size);
    }
}
