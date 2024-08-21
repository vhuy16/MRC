using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.EntityFrameworkCore;
using MRC_API.Constant;
using MRC_API.Payload.Request.Order;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Response.Order;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using System.Transactions;

namespace MRC_API.Service.Implement
{
    public class OrderService : BaseService<OrderService>, IOrderService
    {
        public OrderService(IUnitOfWork<MrcContext> unitOfWork, ILogger<OrderService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateOrderResponse> CreateOrder(List<OrderDetailRequest> orderDetailRequests)
        {
            try
            {
                Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
                if (userId == null)
                {
                    throw new BadHttpRequestException("User ID cannot be null.");
                }

                // Validate quantities in request
                foreach (var orderDetail in orderDetailRequests)
                {
                    if (orderDetail.quantity <= 0)
                    {
                        throw new BadHttpRequestException($"Invalid quantity {orderDetail.quantity} for product ID {orderDetail.productId}. Quantity must be greater than zero.");
                    }
                }

                // Fetch all product details at once to minimize database calls
                var productIds = orderDetailRequests.Select(od => od.productId).ToList();
                var productsList = await _unitOfWork.GetRepository<Product>()
                                                    .GetListAsync(predicate: p => productIds.Contains(p.Id));

                if (productsList.Count != productIds.Count)
                {
                    throw new BadHttpRequestException("One or more products do not exist.");
                }

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    InsDate = TimeUtils.GetCurrentSEATime(),
                    UpDate = TimeUtils.GetCurrentSEATime(),
                    UserId = userId,
                    Status = StatusEnum.Available.GetDescriptionFromEnum(),
                    TotalPrice = 0, // Initialize with 0, we'll calculate this later
                    OrderDetails = new List<OrderDetail>()
                };

                foreach (var orderDetail in orderDetailRequests)
                {
                    var product = productsList.FirstOrDefault(p => p.Id == orderDetail.productId);
                    if (product == null)
                    {
                        throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
                    }

                    if (product.Quantity < orderDetail.quantity)
                    {
                        throw new BadHttpRequestException($"Not enough stock for product {product.ProductName}. Requested: {orderDetail.quantity}, Available: {product.Quantity}");
                    }

                    var newOrderDetail = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        InsDate = TimeUtils.GetCurrentSEATime(),
                        UpDate = TimeUtils.GetCurrentSEATime(),
                        Quantity = orderDetail.quantity,
                        Price = product.Price
                    };

                    product.Quantity -= orderDetail.quantity; // Decrease the quantity of the product

                    order.OrderDetails.Add(newOrderDetail);
                    order.TotalPrice += newOrderDetail.Quantity * newOrderDetail.Price;

                    await _unitOfWork.GetRepository<OrderDetail>().InsertAsync(newOrderDetail);
                    _unitOfWork.GetRepository<Product>().UpdateAsync(product); // Update the product quantity in the database
                }

                await _unitOfWork.GetRepository<Order>().InsertAsync(order);
                bool isSuccessOrder = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessOrder)
                {
                    throw new BadHttpRequestException(MessageConstant.OrderMessage.CreateOrderFail);
                }

                // Prepare response
                var orderDetailsResponse = order.OrderDetails.Select(od =>
                {
                    var product = productsList.First(p => p.Id == od.ProductId);
                    return new CreateOrderResponse.OrderDetailCreateResponse
                    {
                        price = product.Price,
                        productName = product.ProductName,
                        quantity = od.Quantity,
                    };
                }).ToList();

                return new CreateOrderResponse
                {
                    totalPrice = order.TotalPrice,
                    OrderDetails = orderDetailsResponse
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

    }
}