using AutoMapper;
using Business.Interface;
using Microsoft.Extensions.Options;
using MRC_API.Service.Interface;
using Repository.Entity;
using static MRC_API.Utils.PaymentUltils;
using System.Net;
using MRC_API.Configurations;
using MRC_API.Utils;

namespace MRC_API.Service.Implement
{
    public class VNPayService : BaseService<VNPayService>, IVNPayService
    {
        private readonly string _vnpTmnCode;
        private readonly string _vnpHashSecret;
        private readonly string _vnpReturnUrl;
        private readonly string _vnpUrl;
        private readonly IOrderService _orderService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPayService _payService;
        private readonly VNPaySettings _vnpSettings;
        private readonly HttpClient _client;
        private readonly PaymentUltils.Utils utils;
        public VNPayService(IOptions<VNPaySettings> settings,
                            HttpClient client,
                            IUnitOfWork<MrcContext> unitOfWork,
                            ILogger<VNPayService> logger,
                            IMapper mapper,
                            IHttpContextAccessor httpContextAccessor,
                            IOrderService oderServicer,
                            IPayService payService,
                            PaymentUltils.Utils utils)

        : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
            _vnpSettings = settings.Value;
            _client = client;
            _orderService = oderServicer;
            _payService = payService;
            _unitOfWork = unitOfWork;
            _vnpTmnCode = _vnpSettings.vnp_TmnCode;
            _vnpHashSecret = _vnpSettings.vnp_HashSecret;
            _vnpReturnUrl = _vnpSettings.vnp_ReturnUrl;
            _vnpUrl = _vnpSettings.vnp_Url;
            this.utils = utils;
        }
        public async Task<string> CreatePaymentUrl(Guid orderId, decimal shippingFee)
        {
            try
            {

                var order = await _unitOfWork.GetRepository<Order>().SingleOrDefaultAsync(predicate: p => p.Id.Equals(orderId));
                string hostName = Dns.GetHostName();
                string clientIPAddress = utils.GetIpAddress();
                PayLib pay = new PayLib();

                pay.AddRequestData("vnp_Version", PayLib.VERSION);
                pay.AddRequestData("vnp_Command", "pay");
                pay.AddRequestData("vnp_TmnCode", _vnpTmnCode);
                pay.AddRequestData("vnp_Amount", ((int)(order.TotalPrice + shippingFee) * 100000).ToString());
                pay.AddRequestData("vnp_BankCode", "");
                pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
                pay.AddRequestData("vnp_CurrCode", "VND");
                pay.AddRequestData("vnp_IpAddr", clientIPAddress);
                pay.AddRequestData("vnp_Locale", "vn");
                pay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + order.Id);
                pay.AddRequestData("vnp_OrderType", "other");
                pay.AddRequestData("vnp_ReturnUrl", _vnpReturnUrl);
                pay.AddRequestData("vnp_TxnRef", order.Id.ToString());

                string paymentUrl = pay.CreateRequestUrl(_vnpUrl, _vnpHashSecret);
                return paymentUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error creating payment URL: {ex.Message}");
                throw new Exception("Failed to create payment URL", ex);
            }
        }
    }
}
