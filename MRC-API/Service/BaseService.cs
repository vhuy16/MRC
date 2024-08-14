using AutoMapper;
using Business.Interface;
using Repository.Entity;
using System.Security.Claims;

namespace MRC_API.Service
{
    public abstract class BaseService<T> where T : class
    {
        protected IUnitOfWork<MrcContext> _unitOfWork;
        protected ILogger<T> _logger;
        protected IMapper _mapper;
        protected IHttpContextAccessor _httpContextAccessor;

        public BaseService(IUnitOfWork<MrcContext> unitOfWork, ILogger<T> logger, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        protected string GetUsernameFromJwt()
        {
            string username = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return username;
        }

        protected string GetRoleFromJwt()
        {
            string role = _httpContextAccessor?.HttpContext?.User.FindFirstValue(ClaimTypes.Role);
            return role;
        }

        //Use for employee and store manager
        //protected async Task<bool> CheckIsAccount(User user)
        //{
        //    ICollection<User> listAccount = await _unitOfWork.GetRepository<User>().GetListAsync(
        //        predicate: u => s.DelFlg == false);

        //    return listAccount.Select(x => x.Id).Contains(account.Id);
        //}

        //protected string GetAcountIdFromJwt()
        //{
        //    return _httpContextAccessor?.HttpContext?.User?.FindFirstValue("Id");
        //}
    }
}
