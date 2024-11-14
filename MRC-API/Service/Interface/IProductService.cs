﻿using MRC_API.Payload.Request.Product;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Product;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IProductService
    {
        Task<ApiResponse> CreateProduct(CreateProductRequest createProductRequest);
        Task<ApiResponse> GetListProduct(int page, int size, string searchName = null, bool? isAscending = null);
        Task<ApiResponse> GetListProductByCategoryId(Guid CateID, int page, int size);
        //Task<bool> UpdateProduct(Guid ProID, UpdateProductRequest updateProductRequest);
        Task<ApiResponse> UpdateProduct(Guid productId, UpdateProductRequest updateProductRequest);
        Task<ApiResponse> DeleteProduct(Guid ProductId);
        Task<ApiResponse> GetProductById(Guid productId);
    }
}
