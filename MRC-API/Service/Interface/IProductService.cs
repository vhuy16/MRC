using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response.Product;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IProductService
    {
        Task<CreateProductResponse> CreateProduct(CreateProductRequest createProductRequest);
        Task<IPaginate<GetProductResponse>> GetListProduct(int page, int size);
        Task<IPaginate<GetProductResponse>> GetListProductByCategoryId(Guid CateID, int page, int size);
        //Task<bool> UpdateProduct(Guid ProID, UpdateProductRequest updateProductRequest);
        Task<UpdateProductResponse> UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest);
        Task<bool> DeleteProduct(Guid ProductId);
    }
}
