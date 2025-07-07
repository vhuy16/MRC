﻿using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using Microsoft.Extensions.Options;
using MRC_API.Payload.Response.Pay;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Net.payOS;
using Net.payOS.Types;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Repository.Entity;
using Repository.Enum;
using System.Security.Cryptography;
using System.Text;
using MRC_API.Payload.Request.OrderDetail;
using MRC_API.Payload.Request.Payment;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using MRC_API.Payload.Response;
using MRC_API.Constant;
using Microsoft.EntityFrameworkCore;
using System;


namespace MRC_API.Service.Implement
{
    public class PayService : BaseService<PayService>, IPayService
    {

        private readonly PayOS _payOS;
        private readonly PayOSSettings _payOSSettings;
        private readonly HttpClient _client;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOrderService _oderServicer;
        public PayService(IOptions<PayOSSettings> settings, HttpClient client, IUnitOfWork<MrcContext> unitOfWork, ILogger<PayService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor, IOrderService oderServicer)
       : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _payOSSettings = settings.Value; // Lấy giá trị từ IOptions
            _payOS = new PayOS(_payOSSettings.ClientId, _payOSSettings.ApiKey, _payOSSettings.ChecksumKey);
            _client = client;
            _unitOfWork = unitOfWork;
            _oderServicer = oderServicer;
        }
        private string ComputeHmacSha256(string data, string checksumKey)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(checksumKey)))
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }
        //public async Task<ApiResponse> CreatePaymentUrlRegisterCreator(List<Guid> cartItemsId)
        //{
        //    try
        //    {
        //        // Lấy thông tin người dùng
        //        Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
        //        if (userId == null)
        //        {
        //            throw new BadHttpRequestException("User ID is null.");
        //        }

        //        // Ensure _unitOfWork is initialized
        //        if (_unitOfWork == null)
        //        {
        //            throw new InvalidOperationException("UnitOfWork is not initialized.");
        //        }

        //        // Ensure GetRepository<User>() is not null
        //        var userRepository = _unitOfWork.GetRepository<User>();
        //        if (userRepository == null)
        //        {
        //            throw new InvalidOperationException("User repository is not available.");
        //        }

        //        // Ensure StatusEnum and GetDescriptionFromEnum are correct
        //        var statusDescription = StatusEnum.Available.GetDescriptionFromEnum();
        //        if (statusDescription == null)
        //        {
        //            throw new InvalidOperationException("Status description is null.");
        //        }

        //        // Fetch user details
        //        var user = await userRepository.SingleOrDefaultAsync(
        //            predicate: u => u.Id.Equals(userId) && u.Status.Equals(statusDescription));


        //        if (user == null)
        //        {
        //            throw new BadHttpRequestException("You need to log in.");
        //        }

        //        // Lấy thông tin giỏ hàng của người dùng
        //        var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
        //            predicate: c => c.UserId.Equals(userId));

        //        if (cart == null)
        //        {
        //            return new ApiResponse()
        //            {
        //               status = StatusCodes.Status400BadRequest.ToString(),
        //               message = "Cart is empty",
        //               data = null
        //            };
        //        }

        //        List<CartItem> cartItems = new List<CartItem>();
        //        foreach(var cartItemid in cartItemsId)
        //        {
        //            var cartItem = await _unitOfWork.GetRepository<CartItem>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(cartItemid)
        //                                                                                          && p.CartId.Equals(cart.Id));
        //            cartItems.Add(cartItem);
        //        }


        //        int totalPrice = 0;
        //        var items = new List<ItemData>();

        //        // Lấy thông tin sản phẩm từ CartItems
        //        foreach (var cartItem in cartItems)
        //        {

        //                var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
        //                predicate: p => p.Id.Equals(cartItem.ProductId));

        //                if (product == null)
        //                {
        //                    return new ApiResponse()
        //                    {
        //                        status = StatusCodes.Status400BadRequest.ToString(),
        //                        message = MessageConstant.ProductMessage.ProductNotExist,
        //                        data = null
        //                    };
        //                }

        //                // Tạo đối tượng ItemData và thêm vào danh sách
        //                var itemData = new ItemData(product.ProductName, cartItem.Quantity, (int)product.Price);
        //                items.Add(itemData);

        //                // Tính tổng giá trị
        //                totalPrice += cartItem.Quantity * (int)product.Price;

        //            cartItem.Status = StatusEnum.Pending.GetDescriptionFromEnum();
        //            _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);
        //            }
        //          await _unitOfWork.CommitAsync();


        //        // Thông tin người mua
        //        string buyerName = user.FullName;
        //        string buyerPhone = user.PhoneNumber;
        //        string buyerEmail = user.Email;

        //        // Generate an order code and set the description
        //        var orderCode = new Random().Next(1, 1000);
        //        var description = "VQRIO123";

        //        // Create signature data
        //        var signatureData = new Dictionary<string, object>
        //{
        //    { "amount", totalPrice },
        //    { "cancelUrl", _payOSSettings.ReturnUrlFail },
        //    { "description", description },
        //    { "expiredAt", DateTimeOffset.Now.AddMinutes(10).ToUnixTimeSeconds() },
        //    { "orderCode", orderCode },
        //    { "returnUrl", _payOSSettings.ReturnUrl }
        //};

        //        // Sort and compute the signature
        //        var sortedSignatureData = new SortedDictionary<string, object>(signatureData);
        //        var dataForSignature = string.Join("&", sortedSignatureData.Select(p => $"{p.Key}={p.Value}"));
        //        var signature = ComputeHmacSha256(dataForSignature, _payOSSettings.ChecksumKey);

        //        DateTimeOffset expiredAt = DateTimeOffset.Now.AddMinutes(10);

        //        // Tạo instance của PaymentData
        //        var paymentData = new PaymentData(
        //            orderCode: orderCode,
        //            amount: (int)totalPrice,
        //            description: description,
        //            items: items, // Truyền danh sách các ItemData đã tạo
        //            cancelUrl: _payOSSettings.ReturnUrlFail,
        //            returnUrl: _payOSSettings.ReturnUrl,
        //            signature: signature,
        //            buyerName: buyerName,
        //            buyerPhone: buyerPhone,
        //            buyerEmail: buyerEmail,

        //            buyerAddress: "HCM", // Nếu có
        //            expiredAt: (int)expiredAt.ToUnixTimeSeconds()
        //        );

        //        // Gọi API tạo thanh toán
        //        var paymentResult = await _payOS.createPaymentLink(paymentData);
        //        if (paymentResult != null)
        //        {
        //            // Create payment record in the database
        //            var createPaymentRequest = new CreatePaymentRequest
        //            {

        //                Amount = totalPrice,
        //                PaymentMethod = "PayOS",  // Adjust based on your actual method
        //                Status = "Pending",  // You can adjust this status accordingly
        //                UserId = userId.Value
        //            };

        //            var paymentCreated = await CreatePayment(createPaymentRequest);
        //            if (!paymentCreated)
        //            {
        //                throw new Exception("Failed to create payment record.");
        //            }
        //        }

        //        return new ApiResponse()
        //        {
        //            status = StatusCodes.Status200OK.ToString(),
        //            message = "Successful",
        //            data = paymentResult
        //        };
        //    }

        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error creating payment URL.");
        //        throw new BadHttpRequestException("An error occurred while creating the payment URL.", ex);
        //    }
        //}

        public async Task<ApiResponse> CreatePaymentUrlRegisterCreator(Guid orderId)
        {
            var items = new List<ItemData>();
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);

            if (userId == null)
            {
                return new ApiResponse
                {
                    data = string.Empty,
                    message = "User ID is null",
                    status = StatusCodes.Status401Unauthorized.ToString(),
                };
            }

            var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
                predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));
            if (user == null)
            {
                return new ApiResponse
                {
                    data = string.Empty,
                    message = "You need to login",
                    status = StatusCodes.Status400BadRequest.ToString()
                };
            }

            var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(predicate: o => o.Id.Equals(orderId));
            if (order == null)
            {
                return new ApiResponse
                {
                    data = string.Empty,
                    message = "Order not found",
                    status = StatusCodes.Status404NotFound.ToString(),
                };
            }

            var orderDetailIds = order.OrderDetails.Select(od => od.Id).ToList();

            foreach (var orderDetailId in orderDetailIds)
            {
                var orderDetail = await _unitOfWork.GetRepository<OrderDetail>().SingleOrDefaultAsync(
                    predicate: o => o.Id.Equals(orderDetailId),
                    include: od => od.Include(od => od.Product) // Assuming there's a navigation property for Product
                );

                if (orderDetail != null && orderDetail.Product != null)
                {
                    var price = orderDetail.Price; // Assuming `Price` is a property in `OrderDetail`
                    var productName = orderDetail.Product.ProductName; // Assuming `ProductName` is a property in `Product`
                    var quantity = orderDetail.Quantity; // Assuming `Quantity` is a property in `OrderDetail`

                    var itemData = new ItemData(productName, (int)quantity, (int)price);
                    items.Add(itemData);
                }
            }

            string buyerName = user.FullName;
            string buyerPhone = user.PhoneNumber;
            string buyerEmail = user.Email;
            Random random = new Random();
            long orderCode = (DateTime.Now.Ticks % 1000000000000000L) * 10 + random.Next(0, 1000);
            var description = "VQRIO123";
            var totalPrice = order.TotalPrice;
            var signatureData = new Dictionary<string, object>
    {
        { "amount", totalPrice },
        { "cancelUrl", _payOSSettings.ReturnUrlFail },
        { "description", description },
        { "expiredAt", DateTimeOffset.Now.AddMinutes(10).ToUnixTimeSeconds() },
        { "orderCode", orderCode },
        { "returnUrl", _payOSSettings.ReturnUrl }
    };
            var sortedSignatureData = new SortedDictionary<string, object>(signatureData);
            var dataForSignature = string.Join("&", sortedSignatureData.Select(p => $"{p.Key}={p.Value}"));
            var signature = ComputeHmacSha256(dataForSignature, _payOSSettings.ChecksumKey);

            DateTimeOffset expiredAt = DateTimeOffset.Now.AddMinutes(10);
            var paymentData = new PaymentData(
                orderCode: orderCode,
                amount: (int)totalPrice,
                description: description,
                items: items,
                cancelUrl: _payOSSettings.ReturnUrlFail,
                returnUrl: _payOSSettings.ReturnUrl,
                signature: signature,
                buyerName: buyerName,
                buyerPhone: buyerPhone,
                buyerEmail: buyerEmail,
                buyerAddress: "HCM", // Optional field
                expiredAt: (int)expiredAt.ToUnixTimeSeconds()
            );

            var paymentResult = await _payOS.createPaymentLink(paymentData);

            if (paymentResult != null)
            {
                // Create payment record in the database
                var createPaymentRequest = new CreatePaymentRequest
                {
                    Amount = totalPrice,
                    PaymentMethod = "PayOS",  // Adjust based on your actual method
                    Status = "Pending",  // You can adjust this status accordingly
                    UserId = userId.Value,
                    OrderCode = orderCode,
                    OrderId = orderId,
                };

                var paymentCreated = await CreatePayment(createPaymentRequest);
                if (!paymentCreated)
                {
                    throw new Exception("Failed to create payment record.");
                }

                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Successful",
                    data = paymentResult
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status400BadRequest.ToString(),
                message = "Failed to create payment link",
                data = string.Empty
            };
        }

        //public async Task<ExtendedPaymentInfo> GetPaymentInfo(string paymentLinkId)
        //{
        //    try
        //    {
        //        var getUrl = $"https://api-merchant.payos.vn/v2/payment-requests/{paymentLinkId}";

        //        // Create a new HttpRequestMessage
        //        var request = new HttpRequestMessage(HttpMethod.Get, getUrl);
        //        request.Headers.Add("x-client-id", _payOSSettings.ClientId);
        //        request.Headers.Add("x-api-key", _payOSSettings.ApiKey);

        //        // Send the request
        //        var response = await _client.SendAsync(request);

        //        // Ensure the request is successful
        //        response.EnsureSuccessStatusCode();

        //        // Read the response content
        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        // Deserialize the response JSON to JObject
        //        var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
        //        var paymentInfo = responseObject["data"].ToObject<ObjectPayment>();

        //        // Fetch payment information from the external service
        //        // Lấy thông tin người dùng
        //        Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
        //        var user = await _unitOfWork.GetRepository<User>().SingleOrDefaultAsync(
        //            predicate: u => u.Id.Equals(userId) && u.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

        //        if (user == null)
        //        {
        //            throw new BadHttpRequestException("You need to log in.");
        //        }

        //        // Lấy thông tin giỏ hàng của người dùng
        //        var cart = await _unitOfWork.GetRepository<Cart>().SingleOrDefaultAsync(
        //            predicate: c => c.UserId.Equals(userId));

        //        if (cart == null)
        //        {
        //            throw new BadHttpRequestException("Cart is empty.");
        //        }

        //        var cartItems = await _unitOfWork.GetRepository<CartItem>().GetListAsync(
        //            predicate: p => p.CartId.Equals(cart.Id)
        //                        && p.Status.Equals(StatusEnum.Pending.GetDescriptionFromEnum()));

        //        int totalPrice = 0;
        //        var items = new List<CustomItemData>();

        //        // Lấy thông tin sản phẩm từ CartItems
        //        foreach (var cartItem in cartItems)
        //        {
        //            var product = await _unitOfWork.GetRepository<Product>().SingleOrDefaultAsync(
        //                predicate: p => p.Id.Equals(cartItem.ProductId));

        //            if (product == null)
        //            {
        //                throw new BadHttpRequestException($"Product with ID {cartItem.ProductId} does not exist.");
        //            }

        //            // Tạo đối tượng ItemData và thêm vào danh sách
        //            var itemData = new CustomItemData(product.ProductName, cartItem.Quantity, (int)product.Price, product.Id);
        //            items.Add(itemData);
        //            // Tính tổng giá trị
        //            totalPrice += cartItem.Quantity * (int)product.Price;
        //        }

        //        // Thông tin người mua
        //        string buyerName = user.FullName;
        //        string buyerPhone = user.PhoneNumber;
        //        string buyerEmail = user.Email;

        //        var extendedPaymentInfo = new ExtendedPaymentInfo
        //        {
        //            Amount = totalPrice,
        //            Description = "VQRIO123",
        //            Items = items,
        //            BuyerName = buyerName,
        //            BuyerPhone = buyerPhone,
        //            BuyerEmail = buyerEmail,
        //            Status = paymentInfo.Status,

        //            // Add other properties as needed
        //        };

        //        // Update product status if payment is completed
        //        if (paymentInfo.Status == "PAID")
        //        {
        //            foreach (var cartItem in cartItems)
        //            {
        //                var product = await _unitOfWork.GetRepository<Product>()
        //                    .SingleOrDefaultAsync(predicate: p => p.Id.Equals(cartItem.ProductId));

        //                if (product != null)
        //                {
        //                    // Update product quantity
        //                    product.Quantity -= cartItem.Quantity;
        //                    _unitOfWork.GetRepository<Product>().UpdateAsync(product);

        //                    // Update cart item status to "Paid"
        //                    cartItem.Status = StatusEnum.Paid.GetDescriptionFromEnum();
        //                    _unitOfWork.GetRepository<CartItem>().UpdateAsync(cartItem);
        //                }
        //            }

        //            // Save changes to the database
        //            await _unitOfWork.CommitAsync();
        //        }
        //        return extendedPaymentInfo;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "An error occurred while getting payment info.");
        //        throw new BadHttpRequestException("An error occurred while getting payment info.", ex);
        //    }
        //}
        public async Task<bool> CreatePayment(CreatePaymentRequest createPaymentRequest)
        {
            var payment = new Payment
            {
                PaymentId = Guid.NewGuid(),
                Amount = (decimal)createPaymentRequest.Amount,
                CreatedAt = TimeUtils.GetCurrentSEATime(),
                UpdatedAt = TimeUtils.GetCurrentSEATime(),
                PaymentMethod = createPaymentRequest.PaymentMethod,
                Status = createPaymentRequest.Status,
                UserId = createPaymentRequest.UserId,
                OrderCode = createPaymentRequest.OrderCode,
                OrderId = createPaymentRequest.OrderId,
            };
            await _unitOfWork.GetRepository<Payment>().InsertAsync(payment);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<ApiResponse> HandlePaymentCallback(string paymentLinkId, long orderCode)
        {
            try
            {
                var paymentInfo = await GetPaymentInfo(paymentLinkId);

                if (paymentInfo.Status == "PAID")
                {
                    var payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(
                        predicate: p => p.OrderCode.Equals(orderCode));

                    if (payment != null)
                    {
                        payment.Status = StatusEnum.Paid.GetDescriptionFromEnum();
                        var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(
                            predicate: o => o.Id.Equals(payment.OrderId));
                        order.Status = OrderStatus.PENDING_DELIVERY.GetDescriptionFromEnum();
                        _unitOfWork.GetRepository<Payment>().UpdateAsync(payment);
                        _unitOfWork.GetRepository<Order>().UpdateAsync(order);
                        await _unitOfWork.CommitAsync();
                        return new ApiResponse()
                        {
                            status = StatusCodes.Status200OK.ToString(),
                            message = "Thanh toán thành công",
                            data = true
                        };
                    }
                }
                else if(paymentInfo.Status == "CANCELLED")
                {
                    var payment = await _unitOfWork.GetRepository<Payment>().SingleOrDefaultAsync(
                        predicate: p => p.OrderCode.Equals(orderCode));

                    if (payment != null)
                    {
                        payment.Status = StatusEnum.Cancelled.GetDescriptionFromEnum();
                        _unitOfWork.GetRepository<Payment>().UpdateAsync(payment);
                        await _unitOfWork.CommitAsync();
                        return new ApiResponse()
                        {
                            status = StatusCodes.Status200OK.ToString(),
                            message = "Hủy thanh toán",
                            data = true
                        };
                    }
                }
                return new ApiResponse()
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "Thanh toán không thành công",
                    data = false
                };
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while handling payment callback.", ex);
            }
        }

        public async Task<ExtendedPaymentInfo> GetPaymentInfo(string paymentLinkId)
        {
            try
            {
                var getUrl = $"https://api-merchant.payos.vn/v2/payment-requests/{paymentLinkId}";

                var request = new HttpRequestMessage(HttpMethod.Get, getUrl);
                request.Headers.Add("x-client-id", _payOSSettings.ClientId);
                request.Headers.Add("x-api-key", _payOSSettings.ApiKey);

                // Gửi yêu cầu HTTP
                var response = await _client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(responseContent);
                var paymentInfo = responseObject["data"].ToObject<ExtendedPaymentInfo>();

                return paymentInfo;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while getting payment info.", ex);
            }
            return new ExtendedPaymentInfo();
        }
    }
}
