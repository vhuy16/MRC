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
            // Validate if the CartItem list is empty
            if (createOrderRequest.CartItem == null || !createOrderRequest.CartItem.Any())
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "No Cart Items provided. Please add items to your cart before placing an order.",
                    data = null
                };
            }

            try
            {
                var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(predicate: p => p.UserId.Equals(userId), include: query => query.Include(c => c.User));
                var cartItems = new List<CartItem>();
                foreach (var cartItemId in createOrderRequest.CartItem)
                {
                    var cartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(predicate: p => p.CartId.Equals(cart.Id)
                                                                                                        && p.Id.Equals(cartItemId)
                                                                                                        && p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
                    if (cartItem != null)
                    {
                        cartItems.Add(cartItem);
                    }
                }
                if (cartItems.Count == 0)
                {
                    return new ApiResponse()
                    {
                        status = StatusCodes.Status400BadRequest.ToString(),
                        message = "None of the Cart Items are available for checkout. Please verify your cart.",
                        data = null
                    };
                }
                decimal totalprice = 0;
                if (cartItems.Count == 0)
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
                    Status = OrderStatus.PENDING_PAYMENT.GetDescriptionFromEnum(),
                    ShipCost = createOrderRequest.ShipCost,
                    Address = createOrderRequest.Address,
                    OrderDetails = new List<OrderDetail>()
                };

                // Add Order details to the order
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

                // Insert the Order into the database
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
                    var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
                        predicate: p => p.Id.Equals(od.ProductId));
                    if (product != null)
                    {
                        orderDetailsResponse.Add(new CreateOrderResponse.OrderDetailCreateResponse
                        {
                            Price = od.Price,
                            ProductName = product.ProductName,
                            Quantity = od.Quantity
                        });
                    }
                }
                order = await _unitOfWork.GetRepository<Order>()
           .SingleOrDefaultAsync(predicate: p => p.Id.Equals(order.Id), include: query => query.Include(o => o.User));

                // Check if order or user is still null

                if (order == null || order.User == null)
                {
                    throw new Exception("Order or User information is missing.");
                }

                if (string.IsNullOrEmpty(order.User.UserName) || string.IsNullOrEmpty(order.User.Email))
                {
                    throw new Exception("User name or email is missing.");
                }

                if (order.ShipCost == null)
                {
                    throw new Exception("Ship cost is missing.");
                }

                if (string.IsNullOrEmpty(order.Address))
                {
                    throw new Exception("Order address is missing.");
                }

                CreateOrderResponse createOrderResponse = new CreateOrderResponse
                {
                    Id = order.Id,
                    OrderDetails = orderDetailsResponse,
                    ShipCost = order.ShipCost,
                    TotalPrice = order.TotalPrice,
                    Address = order.Address,
                    userResponse = new CreateOrderResponse.UserResponse
                    {
                        Name = order.User.UserName,
                        Email = order.User.Email,
                        PhoneNumber = order.User.PhoneNumber
                    }
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
                throw;
            }
            catch (Exception ex)
            {
                throw new BadHttpRequestException("An unexpected error occurred while creating the order.", ex);
            }
        }

        public async Task<ApiResponse> GetListOrder (int page, int size, bool? isAscending)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            if (userId == null)
            {
                throw new BadHttpRequestException("User ID cannot be null.");
            }
            var orders = await _unitOfWork.GetRepository<Order>().GetPagingListAsync(
                selector: s => new GetOrderResponse
                {
                    OrderId = s.Id,
                    TotalPayment = s.TotalPrice,
                    Status = s.Status,
                    Address = s.Address,
                    ShipCost = s.ShipCost,
                    OrderDate = s.InsDate,
                    User = new GetOrderResponse.UserResponse
                    {
                        Name = s.User.UserName,  // Fetch user's name
                        Email = s.User.Email,    // Fetch user's email
                        PhoneNumber = s.User.PhoneNumber, // Fetch user's phone number
                        FullName = s.User.FullName
                    },
                    OrderDetails = s.OrderDetails.Select(od => new GetOrderResponse.OrderDetailCreateResponseModel
                    {
                        
                        Price = od.Price,
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity
                    }).ToList(),
                    TotalPrice = s.OrderDetails.Sum(od => od.Price * od.Quantity)
                },
                page: page,
                size: size,
                predicate: od => od.UserId.Equals(userId),
                orderBy: q => isAscending.HasValue
                ? (isAscending.Value ? q.OrderBy(p => p.InsDate) : q.OrderByDescending(p => p.InsDate))
            : q.OrderByDescending(p => p.InsDate)
                );
            int totalItems = orders.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (orders == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "List Cart Item",
                    data = new Paginate<Order>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Order>()
                    }
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "List Cart Item",
                data = orders
            };
        }
        public async Task<ApiResponse> GetAllOrder(int page, int size, string status, bool? isAscending, string userName)
        {
            //decimal? sum = 0;
            var orders = await _unitOfWork.GetRepository<Order>().GetPagingListAsync(
                selector: s => new GetOrderResponse
                {
                    OrderId = s.Id,
                    TotalPayment = s.TotalPrice,    
                    Address = s.Address,
                    ShipCost = s.ShipCost,
                    Status = s.Status,
                    OrderDate = s.InsDate,
                    User = new GetOrderResponse.UserResponse
                    {
                        Name = s.User.UserName,  // Fetch user's name
                        Email = s.User.Email,    // Fetch user's email
                        PhoneNumber = s.User.PhoneNumber, // Fetch user's phone number
                        FullName = s.User.FullName
                    },
                    OrderDetails = s.OrderDetails.Select(od => new GetOrderResponse.OrderDetailCreateResponseModel
                    {
                        Price = od.Price,
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity,
                    }).ToList(),
                    TotalPrice = s.OrderDetails.Sum(od => od.Price * od.Quantity)

                },
                page: page,
                size: size,
                predicate: od => (string.IsNullOrEmpty(status) || od.Status.Equals(status)) &&
                                 (string.IsNullOrEmpty(userName) || od.User.UserName.Contains(userName)), // Added userName search condition
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.InsDate) : q.OrderByDescending(p => p.InsDate))
                    : q.OrderByDescending(p => p.InsDate)
            );

            //foreach(var o in orders.Items)
            //{
            //    foreach(var od in o.OrderDetails)
            //    {
            //        sum += od.Price * od.Quantity;

            //    }
            //}

            int totalItems = orders.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);

            if (orders == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "List Cart Item",
                    data = new Paginate<Order>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Order>()
                    }
                };
            }

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "List Cart Item",
                data = orders
            };
        }

        public async Task<ApiResponse> GetOrderById(Guid id)
        {
            var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(
                selector: o => new GetOrderResponse()
                {
                    OrderId = o.Id,
                    TotalPayment = o.TotalPrice,
                    Address = o.Address,
                    Status = o.Status,
                    ShipCost = o.ShipCost,
                    OrderDate = o.InsDate,
                    OrderDetails = o.OrderDetails.Select(od => new GetOrderResponse.OrderDetailCreateResponseModel
                    {
                        Price = od.Price,
                        ProductName = od.Product.ProductName,
                        Quantity = od.Quantity
                    }).ToList(),
                    User = new GetOrderResponse.UserResponse
                    {
                        Name = o.User.UserName,
                        Email = o.User.Email, 
                        PhoneNumber = o.User.PhoneNumber,
                        FullName = o.User.FullName
                    },
                    TotalPrice = o.OrderDetails.Sum(od => od.Price * od.Quantity)
                },
                predicate: o => o.Id.Equals(id));
            if(order == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Không tìm thấy đơn hàng",
                    data = null
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Đơn hàng",
                data = order
            };
        }

        public async Task<ApiResponse> UpdateOrder(Guid id, OrderStatus? orderStatus, ShipEnum? shipStatus)
        {
            var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(
                predicate: o => !o.Status.Equals(OrderStatus.PENDING_PAYMENT.GetDescriptionFromEnum()) && o.Id.Equals(id));
            if(order == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Đơn hàng này không tồn tại",
                    data = null
                };
            }
            
            //if (order.Status.Equals(OrderStatus.SHIPPING.GetDescriptionFromEnum()))
            //{
            //    order.Status = OrderStatus.PENDING_DELIVERY.GetDescriptionFromEnum();
            //}
            //else if (order.Status.Equals(OrderStatus.PENDING_DELIVERY.GetDescriptionFromEnum()))
            //{
            //    order.Status = OrderStatus.COMPLETED.GetDescriptionFromEnum();
            //}
            if (order.Status.Equals(OrderStatus.COMPLETED.GetDescriptionFromEnum()))
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "Đơn hàng đã hoàn thành",
                    data = false
                };
            }
            if (order.Status.Equals(OrderStatus.PENDING_DELIVERY.GetDescriptionFromEnum()) &&
    orderStatus.HasValue &&
    !orderStatus.Value.Equals(OrderStatus.SHIPPING))
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "Đơn hàng đang chờ giao chỉ có thể cập nhật lên trạng thái SHIPPING",
                    data = false
                };
            }
            order.Status = orderStatus.Value.GetDescriptionFromEnum();
            if (shipStatus.HasValue)
            {
                order.ShipStatus = (int?)shipStatus;
            }
            _unitOfWork.GetRepository<Order>().UpdateAsync(order);
            await _unitOfWork.CommitAsync();
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Cập nhật đơn hàng thành công",
                data = true
            };
        }

        public async Task<ApiResponse> CancelOrder(Guid id)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                throw new BadHttpRequestException("You need log in.");
            }
            var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(
                predicate: o => o.Status.Equals(OrderStatus.PENDING_PAYMENT.GetDescriptionFromEnum()) && o.UserId.Equals(userId));
            if(order == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "Đơn hàng đã thanh toán hoặc không tồn tại",
                    data = false
                };
            }
            order.Status = OrderStatus.CANCELLED.GetDescriptionFromEnum();
            _unitOfWork.GetRepository<Order>().UpdateAsync(order);
            bool isSuccess = await _unitOfWork.CommitAsync() > 0;
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Hủy đơn hàng thành công",
                data = isSuccess
            };
        }
    }
}