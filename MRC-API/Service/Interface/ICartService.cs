using MRC_API.Payload.Response.Cart;

namespace MRC_API.Service.Interface
{
    public interface ICartService
    {
        Task<CreateNewCartResponse> CreateCart();
    }
}
