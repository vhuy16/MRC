using AutoMapper;
using Business.Interface;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Dashboard;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;

namespace MRC_API.Service.Implement
{
    public class DashBoardService : BaseService<DashBoardService>, IDashBoardService
    {
        public DashBoardService(IUnitOfWork<MrcContext> unitOfWork, ILogger<DashBoardService> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> GetDashBoard(int? month, int? year)
        {
            var users = await _unitOfWork.GetRepository<User>().GetListAsync();
            var categories = await _unitOfWork.GetRepository<Category>().GetListAsync();
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync();
            var totalRevenue = (await _unitOfWork.GetRepository<Order>().GetListAsync(
                                predicate: o => (!month.HasValue || (o.InsDate.HasValue && o.InsDate.Value.Month == month.Value)) &&
                                                (!year.HasValue || (o.InsDate.HasValue && o.InsDate.Value.Year == year.Value)) &&
                                                !o.Status.Equals(OrderStatus.PENDING_PAYMENT.GetDescriptionFromEnum())
                                )).Sum(o => o.TotalPrice);
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Dashboard",
                data = new GetDashBoardResponse()
                {
                    TotalUsers = users.Count,
                    TotalCategories = categories.Count,
                    Categories = categories.Select(c => new GetDashBoardResponse.CategoryDetail()
                    {
                        CategoryName = c.CategoryName,
                    }).ToList(),
                    TotalProducts = products.Count,
                    Products = products.Select(p => new GetDashBoardResponse.ProductDetail()
                    {
                        ProductName = p.ProductName,
                        Quantity = p.Quantity,
                        Price = p.Price,
                    }).ToList(),
                    TotalRevenue = totalRevenue,
                }
            };
        }
    }
}
