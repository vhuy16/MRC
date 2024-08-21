using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Payload.Response.Cart;
using MRC_API.Service.Interface;
using Repository.Entity;

namespace MRC_API.Service.Implement
{
    public class CartService : BaseService<Cart>, ICartService
    {
        public CartService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Cart> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewCartResponse> CreateCart()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            Cart cart = new Cart()
            {
                Id = Guid.NewGuid(),
                UserId = userId.Value,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime()
            };
            await _unitOfWork.GetRepository<Cart>().InsertAsync(cart);
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewCartResponse createNewCartResponse = null;
            if (isSuccesfully)
            {
                createNewCartResponse = new CreateNewCartResponse()
                {
                    Id = cart.Id
                };
            }
            return createNewCartResponse;
        }
    }
}
