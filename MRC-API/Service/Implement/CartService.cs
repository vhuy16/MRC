using AutoMapper;
using Azure;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.CartItem;
using MRC_API.Payload.Response;
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

        public async Task<ApiResponse> AddCartItem(AddCartItemRequest addCartItemRequest)
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
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ProductMessage.ProductNotExist,
                    data = null
                };
                //throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
            }

            if (product.Quantity < addCartItemRequest.Quantity)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.ProductMessage.ProductNotEnough,
                    data = null
                };
            }

            if (addCartItemRequest.Quantity <= 0)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.CartMessage.NegativeQuantity,
                    data = null
                };
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
            AddCartItemResponse? addCartItemResponse = null;
            if (isSuccesfully)
            {
                product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(include: query => query.Include(p => p.SubCategory).Include(p => p.Images),
                                                                                           predicate: p => p.Id.Equals(addCartItemRequest.ProductId)
                                                                                           && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum())); // Ensure Category is loaded

                addCartItemResponse = new AddCartItemResponse() 
                {
                    CartItemId = existingCartItem?.Id ?? cart.Id,
                    ProductId = product.Id,
                    ProductName = product.ProductName,
                    Price = product.Price * addCartItemRequest.Quantity,
                    Images = product.Images.Select(i => i.LinkImage).ToList(),
                    CategoryName = product.SubCategory.SubCategoryName,
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Add cart item successful",
                data = addCartItemResponse
            };
        }

        public async Task<ApiResponse> ClearCart()
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
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Clear cart successful",
                data = true
            };
        }

        public async Task<ApiResponse> DeleteCartItem(Guid itemId)
        {
            // Get the user ID from the HTTP context
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);

            // Validate the user
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("You need to log in.");
            }

            // Retrieve the cart associated with the user
            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));

            // Retrieve the cart item with eager loading for related entities
            var cartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(
                predicate: c => c.CartId.Equals(cart.Id)
                             && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum())
                             && c.Id.Equals(itemId),
                include: ci => ci.Include(ci => ci.Product)
                                 .ThenInclude(p => p.SubCategory)
                                 .Include(ci => ci.Product.Images));

            if (cartItem == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CartMessage.CartItemNotExist,
                    data = null
                };
            }

            // Mark the cart item as unavailable
            cartItem.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            cartItem.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);

            // Commit the changes
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            // Prepare the response data
            if (isSuccessful)
            {
                var response = new GetAllCartItemResponse
                {
                    CartItemId = cartItem.Id,
                    ProductId = cartItem.ProductId,
                    ProductName = cartItem.Product.ProductName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price,
                    Price = cartItem.Product.Price * cartItem.Quantity,
                    Images = cartItem.Product.Images?.Select(i => i.LinkImage).ToList() ?? new List<string>(),
                    CategoryName = cartItem.Product.SubCategory?.SubCategoryName ?? "Unknown"
                };

                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Delete successful",
                    data = response
                };
            }

            // Handle failure scenario
            return new ApiResponse()
            {
                status = StatusCodes.Status500InternalServerError.ToString(),
                message = "Failed to delete the cart item",
                data = null
            };
        }

        public async Task<ApiResponse> GetAllCartItem()
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
           include: c => c.Include(ci => ci.Product) // Include Product
                         .ThenInclude(p => p.SubCategory) // Include Category of Product
                         .Include(ci => ci.Product.Images), // Include Images of Product
           orderBy: c => c.OrderByDescending(ci => ci.InsDate));

            if (cartItems == null || !cartItems.Any())
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "CartItem list",
                    data = new LinkedList<CartItem>()
                };
            }

            var response = cartItems.Select(cartItem => new GetAllCartItemResponse
            {
                CartItemId = cartItem.Id,
                SubCategoryId = cartItem.Product.SubCategoryId,
                SubCategoryName = cartItem.Product.SubCategory.SubCategoryName,
                ProductId = cartItem.ProductId,
                ProductName = cartItem.Product.ProductName,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.Product.Price,
                Price = cartItem.Product.Price * cartItem.Quantity,
                Images = cartItem.Product.Images.Select(i => i.LinkImage).ToList(),
                CategoryName = cartItem.Product.SubCategory.SubCategoryName,
            }).ToList();
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "CartItem list",
                data = response
            };
        }

        public async Task<ApiResponse> GetCartSummary()
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            if (userId == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status401Unauthorized.ToString(),
                    message = "User ID not found.",
                    data = null
                };
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (user == null)
            {
                throw new BadHttpRequestException("User not available.");
            }

            var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
                predicate: c => c.UserId.Equals(userId));


            var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
                predicate: c => c.CartId.Equals(cart.Id) && c.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                include: c => c.Include(c => c.Product));

            if (cartItems == null || !cartItems.Any())
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Summary",
                    data = new CartSummayResponse()
                    {
                        TotalItems = 0,
                        TotalPrice = 0
                    }
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

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Summary",
                data = response
            };
        }


        public async Task<ApiResponse> UpdateCartItem(Guid id, UpdateCartItemRequest updateCartItemRequest)
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
        predicate: ci => ci.Id.Equals(id) && ci.CartId.Equals(cart.Id) && ci.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
        include: ci => ci.Include(ci => ci.Product).ThenInclude(p => p.SubCategory).Include(ci => ci.Product.Images));

            if (existingCartItem == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.CartMessage.CartItemNotExist.ToString(),
                    data = null
                };
            }

            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(existingCartItem.ProductId) && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (updateCartItemRequest.Quantity <= 0)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.CartMessage.NegativeQuantity.ToString(),
                    data = null
                };
            }

            if (product.Quantity < updateCartItemRequest.Quantity)
            {
                var response = new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    data = new UpdateCartItemResponse
                    {
                        CartItemId = existingCartItem.Id,
                        ProductId = existingCartItem.Product.Id,
                        Quantity = existingCartItem.Quantity,
                        ProductName = existingCartItem.Product.ProductName,
                        UnitPrice = existingCartItem.Product.Price,
                        Price = existingCartItem.Product.Price * existingCartItem.Quantity,
                        Images = existingCartItem.Product.Images.Select(i => i.LinkImage).ToList(),
                        CategoryName = existingCartItem.Product.SubCategory.SubCategoryName
                    }
                };

                response.WarnMessage = "Only have " + product.Quantity + " items";

                return response;
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
                    CartItemId = existingCartItem.Id,
                    ProductId = existingCartItem.Product.Id,
                    Quantity = existingCartItem.Quantity,
                    ProductName = existingCartItem.Product.ProductName,
                    UnitPrice = existingCartItem.Product.Price,
                    Price = existingCartItem.Product.Price * existingCartItem.Quantity,
                    Images = existingCartItem.Product.Images.Select(i => i.LinkImage).ToList(),
                    CategoryName = existingCartItem.Product.SubCategory.SubCategoryName
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "update successful",
                data = updateCartItemResponse
            };
        }
    }
}
