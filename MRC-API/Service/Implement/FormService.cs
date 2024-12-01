using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Form;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Form;
using MRC_API.Service.Interface;
using Repository.Entity;
using Repository.Paginate;

namespace MRC_API.Service.Implement
{
    public class FormService : BaseService<Form>, IFormService
    {
        public FormService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Form> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateForm(CreateFormRequest request)
        {
            var newForm = new Form()
            {
                Id = Guid.NewGuid(),
                CompanyName = request.CompanyName,
                Email = request.Email,
                ServiceType = request.ServiceType,
                Question = request.Question,
                DateSent = DateOnly.FromDateTime(DateTime.Now),
            };
            await _unitOfWork.GetRepository<Form>().InsertAsync(newForm);
            await _unitOfWork.CommitAsync();

            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Send Form successful",
                data = new CreateFormResponse()
                {
                    Id = newForm.Id,
                    CompanyName = request.CompanyName,
                    Email = request.Email,
                    ServiceType = request.ServiceType,
                    Question = request.Question,
                    DateSent = newForm.DateSent,
                }

            };
        }

        public async Task<ApiResponse> DeleteForm(Guid id)
        {
            var form = await _unitOfWork.GetRepository<Form>().SingleOrDefaultAsync(
                predicate: f => f.Id.Equals(id));
            if (form == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Không tìm thấy form này",
                    data = false
                };
            }
            _unitOfWork.GetRepository<Form>().DeleteAsync(form);
            await _unitOfWork.CommitAsync();
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Delete Successful",
                data = true
            };
        }

        public async Task<ApiResponse> GetForm(Guid id)
        {
            var form = await _unitOfWork.GetRepository<Form>().SingleOrDefaultAsync(
                predicate: f => f.Id.Equals(id));
            if(form == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = "Không tìm thấy form này",
                    data = null
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Form",
                data = form
            };
        }

        public async Task<ApiResponse> GetForms(int page, int size, string serviceType)
        {
            var forms = await _unitOfWork.GetRepository<Form>().GetPagingListAsync(
                selector: f => new GetFormsResponse()
                {
                    Id = f.Id,
                    CompanyName = f.CompanyName,
                    Email = f.Email,
                    ServiceType = f.ServiceType,
                    Question = f.Question,
                    DateSent = f.DateSent
                },
                page: page,
                size: size,
                predicate: f => string.IsNullOrEmpty(serviceType) || f.ServiceType.Contains(serviceType),
                orderBy: f => f.OrderBy(f => f.DateSent));
            int totalItems = forms.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (forms == null)
            {
                return new ApiResponse()
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "List Form",
                    data = new Paginate<Form>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Form>()
                    }
                };
            }
            return new ApiResponse()
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "List Form",
                data = forms
            };
        }
    }
}
