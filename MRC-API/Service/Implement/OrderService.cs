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
                List<Product> productsList = new List<Product>();
                Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);

                if (userId == null)
                {
                    throw new BadHttpRequestException("User ID cannot be null.");
                }
                foreach (var orderDetail in orderDetailRequests)
                {
                    var productCheck = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(orderDetail.productId));
                    productsList.Add(productCheck);
                }
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    InsDate = TimeUtils.GetCurrentSEATime(),
                    UpDate = TimeUtils.GetCurrentSEATime(),
                    UserId = userId,
                    Status = StatusEnum.Available.GetDescriptionFromEnum(),
                };

                decimal? totalPrice = 0;
                foreach (var orderDetail in orderDetailRequests)
                {
                    var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(predicate:
                        p => p.Id.Equals(orderDetail.productId));

                    if (product == null)
                    {
                        throw new BadHttpRequestException(MessageConstant.ProductMessage.ProductNotExist);
                    }

                    OrderDetail newOrderDetail = new OrderDetail
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        ProductId = product.Id,
                        InsDate = TimeUtils.GetCurrentSEATime(),
                        UpDate = TimeUtils.GetCurrentSEATime(),
                        Quantity = orderDetail.quantity,
                        Price = product.Price
                    };
                    product.Quantity = product.Quantity - newOrderDetail.Quantity.Value;
                    _unitOfWork.GetRepository<Product>().UpdateAsync(product);

                    order.OrderDetails.Add(newOrderDetail);
                    await _unitOfWork.GetRepository<OrderDetail>().InsertAsync(newOrderDetail);
                    totalPrice += newOrderDetail.Quantity * newOrderDetail.Price;
                }


                order.TotalPrice = totalPrice;
                await _unitOfWork.GetRepository<Order>().InsertAsync(order);
                bool isSuccessOrder = await _unitOfWork.CommitAsync() > 0;

                if (!isSuccessOrder)
                {
                    throw new BadHttpRequestException(MessageConstant.OrderMessage.CreateOrderFail);
                }

                return new CreateOrderResponse
                {
                    totalPrice = order.TotalPrice,
                    OrderDetails = order.OrderDetails.Select(od =>
                    {
                        var product = productsList.FirstOrDefault(p => p.Id == od.ProductId);
                        return new CreateOrderResponse.OrderDetailCreateResponse
                        {
                            price = product?.Price ?? 0, // Lookup price from list, fallback to 0 if not found
                            productName = product?.ProductName ?? "Unknown Product", // Lookup product name from list, fallback to "Unknown Product"
                            quantity = od.Quantity,
                        };
                    }).ToList()
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
                        else
                        {
                            throw new BadHttpRequestException("The order was updated by another user. Please refresh and try again.");
                        }
                    }
                    else if (entry.Entity is OrderDetail)
                    {
                        // Handle concurrency for OrderDetail
                        throw new BadHttpRequestException("Concurrency conflict occurred for OrderDetail.");
                    }
                }
                throw;
            }
            catch (BadHttpRequestException ex)
            {
                // Log or handle the exception as necessary
                throw; // Re-throw the exception if it should propagate
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new BadHttpRequestException("An unexpected error occurred while creating the order.", ex);
            }
        }
    }
}