using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Order;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System.Transactions;

namespace MRC_API.Service.Implement
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        
        public OrderService(IUnitOfWork<MrcContext> unitOfWork, ILogger<OrderService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            
        }

        //public async Task<CreateOrderResponse> CreateOrder(List<OrderDetailRequest> orderDetailRequests)
        //{
        //    try
        //    {
        //        Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
        //        if (userId == null)
        //        {
        //            throw new BadHttpRequestException("User ID cannot be null.");
        //        }

        //        // Validate quantities in request
        //        foreach (var orderDetail in orderDetailRequests)
        //        {
        //            if (orderDetail.quantity <= 0)
        //            {
        //                throw new BadHttpRequestException($"Invalid quantity {orderDetail.quantity} for product ID {orderDetail.productId}. Quantity must be greater than zero.");
        //            }
        //        }

        //        // Fetch all product details at once to minimize database calls
        //        var productIds = orderDetailRequests.Select(od => od.productId).ToList();
        //        var productsList = await _unitOfWork.GetRepository<Product>()
        //                                            .GetListAsync(predicate: p => productIds.Contains(p.Id));

        //        if (productsList.Count != productIds.Count)
        //        {
        //            throw new BadHttpRequestException("One or more products do not exist.");
        //        }

        //        Order order = new Order
        //        {
        //            Id = Guid.NewGuid(),
        //            InsDate = TimeUtils.GetCurrentSEATime(),
        //            UpDate = TimeUtils.GetCurrentSEATime(),
        //            UserId = userId,

        //            Status = StatusEnum.Pending.GetDescriptionFromEnum(),
        //            TotalPrice = 0, // Initialize with 0, we'll calculate this later
        //            OrderDetails = new List<OrderDetail>()
        //        };

        //        foreach (var orderDetail in orderDetailRequests)
        //        {
        //            var product = productsList.FirstOrDefault(p => p.Id == orderDetail.productId);
        //            if (product == null)
        //            {
        //                throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
        //            }

        //            if (product.Quantity < orderDetail.quantity)
        //            {
        //                throw new BadHttpRequestException($"Not enough stock for product {product.ProductName}. Requested: {orderDetail.quantity}, Available: {product.Quantity}");
        //            }

        //            var newOrderDetail = new OrderDetail
        //            {
        //                Id = Guid.NewGuid(),
        //                OrderId = order.Id,
        //                ProductId = product.Id,
        //                InsDate = TimeUtils.GetCurrentSEATime(),
        //                UpDate = TimeUtils.GetCurrentSEATime(),
        //                Quantity = orderDetail.quantity,
        //                Price = product.Price
        //            };
        //            product.Quantity = product.Quantity - newOrderDetail.Quantity.Value;
        //            _unitOfWork.GetRepository<Product>().UpdateAsync(product);

        //            //product.Quantity -= orderDetail.quantity; // Decrease the quantity of the product

        //            order.OrderDetails.Add(newOrderDetail);
        //            order.TotalPrice += newOrderDetail.Quantity * newOrderDetail.Price;
        //            order.PaymentId = orderDetail.paymentId;

        //            await _unitOfWork.GetRepository<OrderDetail>().InsertAsync(newOrderDetail);
        //            //_unitOfWork.GetRepository<Product>().UpdateAsync(product); // Update the product quantity in the database
        //        }


        //        await _unitOfWork.GetRepository<Order>().InsertAsync(order);
        //        bool isSuccessOrder = await _unitOfWork.CommitAsync() > 0;

        //        if (!isSuccessOrder)
        //        {
        //            throw new BadHttpRequestException(MessageConstant.OrderMessage.CreateOrderFail);
        //        }

        //        // Prepare response
        //        var orderDetailsResponse = order.OrderDetails.Select(od =>
        //        {
        //            var product = productsList.First(p => p.Id == od.ProductId);
        //            return new CreateOrderResponse.OrderDetailCreateResponse
        //            {
        //                price = product.Price,
        //                productName = product.ProductName,
        //                quantity = od.Quantity,
        //            };
        //        }).ToList();

        //        return new CreateOrderResponse
        //        {
        //            totalPrice = order.TotalPrice,
        //            OrderDetails = orderDetailsResponse
        //        };
        //    }
        //    catch (DbUpdateConcurrencyException ex)
        //    {
        //        foreach (var entry in ex.Entries)
        //        {
        //            if (entry.Entity is Order)
        //            {
        //                var databaseValues = entry.GetDatabaseValues();
        //                if (databaseValues == null)
        //                {
        //                    throw new BadHttpRequestException("The order was deleted by another user.");
        //                }
        //                throw new BadHttpRequestException("The order was updated by another user. Please refresh and try again.");
        //            }
        //            else if (entry.Entity is OrderDetail)
        //            {
        //                throw new BadHttpRequestException("Concurrency conflict occurred for OrderDetail.");
        //            }
        //        }
        //        throw;
        //    }
        //    catch (BadHttpRequestException)
        //    {
        //        throw; // Re-throw specific exception
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the exception if needed
        //        throw new BadHttpRequestException("An unexpected error occurred while creating the order.", ex);
        //    }
        //}
        public async Task<ApiResponse> CreateOrder(CreateOrderRequest createOrderRequest)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            if (userId == null)
            {
                throw new BadHttpRequestException("User ID cannot be null.");
            }

            try
            {
                var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(predicate: p => p.UserId.Equals(userId));
                var cartItems = new List<CartItem>();
                foreach (var cartItemId in createOrderRequest.CartItem)
                {
                    var cartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(predicate: p => p.CartId.Equals(cart.Id) 
                                                                                                && p.Id.Equals(cartItemId)
                                                                                                && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
                    if(cartItem != null)
                    {
                        cartItems.Add(cartItem);
                    }       
                }                
                
                decimal totalprice = 0;
                if(cartItems.Count == 0)
                {
                    return new ApiResponse()
                    {
                        status = StatusCodes.Status404NotFound.ToString(),
                        message = MessageConstant.CartMessage.CartItemIsEmpty,
                        data = null
                    };
                }
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    TotalPrice = 0,
                    InsDate = TimeUtils.GetCurrentSEATime(),
                    UserId = userId,
                    ShipStatus = (int)ShipEnum.NewOrder,
                    Status = StatusEnum.Available.GetDescriptionFromEnum(),
                    ShipCost = createOrderRequest.ShipCost,
                    OrderDetails = new List<OrderDetail>()
                };
                foreach (var cartItem in cartItems)
                {
                    var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                       predicate: p => p.Id.Equals(cartItem.ProductId));
                    totalprice += cartItem.Quantity * (int)product.Price;                   
                    var newOrderDetail = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        InsDate = TimeUtils.GetCurrentSEATime(),
                        UpDate = TimeUtils.GetCurrentSEATime(),
                        Quantity = cartItem.Quantity,
                        Price = product.Price.Value,
                    };
                    order.OrderDetails.Add(newOrderDetail);
                    await _unitOfWork.GetRepository<OrderDetail>().InsertAsync(newOrderDetail);
                }
                order.TotalPrice = totalprice + createOrderRequest.ShipCost;

                await _unitOfWork.GetRepository<Order>().InsertAsync(order);
                bool isSuccessOrder = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessOrder)
                {
                    return new ApiResponse()
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = MessageConstant.OrderMessage.CreateOrderFail,
                        data = null
                    };
                }

                // Delete CartItems with status "buyed"
              

                foreach (var cartItem in cartItems)
                {
                    _unitOfWork.GetRepository<CartItem>().DeleteAsync(cartItem);
                }
                await _unitOfWork.CommitAsync(); // Commit after deletion

                // Prepare response
                var orderDetailsResponse = new List<CreateOrderResponse.OrderDetailCreateResponse>();
                foreach (var od in order.OrderDetails)
                {
                    var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(od.ProductId));
                    if (product != null)
                    {
                        orderDetailsResponse.Add(new CreateOrderResponse.OrderDetailCreateResponse
                        {
                            price = od.Price,
                            productName = product.ProductName, // Get the product name
                            quantity = od.Quantity
                        });
                    }
                }

                CreateOrderResponse createOrderResponse = new CreateOrderResponse
                {
                    id = order.Id,
                    OrderDetails = orderDetailsResponse,
                    shipCost = order.ShipCost.Value,
                    totalPrice = order.TotalPrice,
                };

                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = MessageConstant.OrderMessage.CreateOrderSuccess,
                    data = createOrderResponse
                };

            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Order)
                    {
                        var databaseValues = entry.GetDatabaseValues();
                        if (databaseValues == null)
                        {
                            throw new BadHttpRequestException("The order was deleted by another user.");
                        }
                        throw new BadHttpRequestException("The order was updated by another user. Please refresh and try again.");
                    }
                    else if (entry.Entity is OrderDetail)
                    {
                        throw new BadHttpRequestException("Concurrency conflict occurred for OrderDetail.");
                    }
                }
                throw;
            }
            catch (BadHttpRequestException)
            {
                throw; // Re-throw specific exception
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                throw new BadHttpRequestException("An unexpected error occurred while creating the order.", ex);
            }
        }

        public async Task<IPaginate<GetOrderResponse>> GetListOrder (int page, int size)
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetPagingListAsync(
                selector: s => new GetOrderResponse
                {
                    OrderId = s.Id,
                    totalPrice = s.TotalPrice,
                    OrderDetails = s.OrderDetails.Select(od => new GetOrderResponse.OrderDetailCreateResponseModel
                    {
                        
                        price = od.Price,
                        productName = od.Product.ProductName,
                        quantity = od.Quantity
                    }).ToList()
                },
                page: page,
                size: size,
                predicate: od => od.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum())
                );
            return orders;
        }

    }
}