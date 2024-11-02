using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.CartItem;
using MRC_API.Payload.Response.Cart;
using MRC_API.Payload.Response.CartItem;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;

namespace MRC_API.Service.Implement
{
    public class CartService : BaseService<Cart>, ICartService
    {
        public CartService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Cart> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<AddCartItemResponse> AddCartItem(AddCartItemRequest addCartItemRequest)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(addCartItemRequest.ProductId) && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if(product == null) 
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
            }

            if (product.Quantity < addCartItemRequest.Quantity)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotEnough);
            }

            var existingCartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(
                predicate: ci => ci.CartId.Equals(cart.Id) && ci.ProductId.Equals(addCartItemRequest.ProductId)
                && ci.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if(existingCartItem != null)
            {
                existingCartItem.Quantity += addCartItemRequest.Quantity;
                existingCartItem.UpDate = TimeUtils.GetCurrentSEATime();

                if(product.Quantity < existingCartItem.Quantity)
                {
                    throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotEnough);
                }
                _unitOfWork.GetRepository<CartItem>().UpdateAsync(existingCartItem);
            }

            else 
            {
                CartItem cartItem = new CartItem()
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = product.Id,
                    Quantity = addCartItemRequest.Quantity,
                    Status = StatusEnum.Available.GetDescriptionFromEnum(),
                    InsDate = TimeUtils.GetCurrentSEATime(),
                    UpDate = TimeUtils.GetCurrentSEATime()
                };

                await _unitOfWork.GetRepository<CartItem>().InsertAsync(cartItem);
            }
            
            bool isSuccesfully = await _unitOfWork.CommitAsync() > 0;
            AddCartItemResponse addCartItemResponse = null;
            if (isSuccesfully)
            {
                addCartItemResponse = new AddCartItemResponse() 
                {
                    CartItemId = existingCartItem?.Id ?? cart.Id,
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price * addCartItemRequest.Quantity,
                };
            }
            return addCartItemResponse;
        }

        public async Task<bool> ClearCart()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                predicate: c => c.CartId.Equals(cart.Id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (cartItems == null || !cartItems.Any())
            {
                throw new BadHttpRequestException(MessageConstant.CartMessage.CartItemIsEmpty);
            }

            foreach (var cartItem in cartItems)
            {
                cartItem.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
                _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);
            }

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<bool> DeleteCartItem(Guid ItemId)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            var cartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(
                predicate: c => c.CartId.Equals(cart.Id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if(cartItem == null)
            {
                throw new BadHttpRequestException(MessageConstant.CartMessage.CartItemNotExist);
            }

            cartItem.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            cartItem.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<List<GetAllCartItemResponse>> GetAllCartItem()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                predicate: c => c.CartId.Equals(cart.Id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                include: c => c.Include(c => c.Product));
            if (cartItems == null || !cartItems.Any())
            {
                return new List<GetAllCartItemResponse>();
            }

            var response = cartItems.Select(cartItem => new GetAllCartItemResponse
            {
                CartItemId = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.ProductName,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price,
                Price = cartItem.Product.Price * cartItem.Quantity,
            }).ToList();
            return response;
        }

        public async Task<CartSummayResponse> GetCartSummary()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            if (userId == null)
            {
                throw new BadHttpRequestException("User not logged in.");
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("User not available.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            if (cart == null)
            {
                return new CartSummayResponse
                {
                    TotalItems = 0,
                    TotalPrice = 0
                };
            }

            var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                predicate: c => c.CartId.Equals(cart.Id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                include: c => c.Include(c => c.Product));

            if (cartItems == null || !cartItems.Any())
            {
                return new CartSummayResponse
                {
                    TotalItems = 0,
                    TotalPrice = 0
                };
            }

            decimal totalPrice = 0;
            decimal totalItems = 0;

            foreach (var cartItem in cartItems)
            {
                totalItems += cartItem.Quantity;
                totalPrice += (cartItem.Product?.Price ?? 0) * cartItem.Quantity;
            }

            var response = new CartSummayResponse()
            {
                TotalItems = totalItems,
                TotalPrice = totalPrice
            };

            return response;
        }


        public async Task<UpdateCartItemResponse> UpdateCartItem(Guid id, UpdateCartItemRequest updateCartItemRequest)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            var existingCartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(
                predicate: ci => ci.Id.Equals(id) && ci.CartId.Equals(cart.Id) 
                && ci.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if(existingCartItem == null)
            {
                throw new BadHttpRequestException(MessageConstant.CartMessage.CartItemNotExist);
            }

            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(existingCartItem.ProductId) && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (updateCartItemRequest.Quantity <= 0)
            {
                throw new BadHttpRequestException(MessageConstant.CartMessage.NegativeQuantity);
            }

            if(product.Quantity < updateCartItemRequest.Quantity)
            {
                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotEnough);
            }

            existingCartItem.Quantity = updateCartItemRequest.Quantity.HasValue ? updateCartItemRequest.Quantity.Value 
                : existingCartItem.Quantity;
            existingCartItem.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<CartItem>().UpdateAsync(existingCartItem);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            UpdateCartItemResponse updateCartItemResponse = new();
            if (isSuccessful)
            {
                updateCartItemResponse = new UpdateCartItemResponse()
                {
                    CartItemId = id,
                    ProductId = product.Id,
                    Quantity = updateCartItemRequest.Quantity,
                    ProductName = product.ProductName,
                    UnitPrice = product.Price,
                    Price = product.Price * updateCartItemRequest.Quantity,
                };
            }
            return updateCartItemResponse;
        }
    }
}
