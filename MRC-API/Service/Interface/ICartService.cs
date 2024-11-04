using MRC_API.Payload.Request.CartItem;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Cart;
using MRC_API.Payload.Response.CartItem;

namespace MRC_API.Service.Interface
{
    public interface ICartService
    {
        Task<ApiResponse> AddCartItem(AddCartItemRequest addCartItemRequest);
        Task<ApiResponse> DeleteCartItem(Guid ItemId);
        Task<ApiResponse> GetAllCartItem();
        Task<ApiResponse> ClearCart();
        Task<ApiResponse> GetCartSummary();
        Task<ApiResponse> UpdateCartItem(Guid id, UpdateCartItemRequest updateCartItemRequest);
    }
}
