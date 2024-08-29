using MRC_API.Payload.Request.CartItem;
using MRC_API.Payload.Response.Cart;
using MRC_API.Payload.Response.CartItem;

namespace MRC_API.Service.Interface
{
    public interface ICartService
    {
        Task<AddCartItemResponse> AddCartItem(AddCartItemRequest addCartItemRequest);
        Task<bool> DeleteCartItem(Guid ItemId);
        Task<List<GetAllCartItemResponse>> GetAllCartItem();
        Task<bool> ClearCart();
        Task<CartSummayResponse> GetCartSummary();
        Task<bool> UpdateCartItem(Guid id, UpdateCartItemRequest updateCartItemRequest);
    }
}
