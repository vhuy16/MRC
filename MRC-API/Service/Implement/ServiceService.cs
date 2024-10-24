using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Service;
using MRC_API.Payload.Response.Service;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System;
using System.Threading.Tasks;

namespace MRC_API.Service.Implement
{
    public class ServiceService : BaseService<Repository.Entity.Service>, IServiceService
    {
        public ServiceService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Repository.Entity.Service> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewServiceResponse> CreateNewService(CreateNewServiceRequest createNewServiceRequest)
        {
            var serviceExist = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.ServiceName.Equals(createNewServiceRequest.ServiceName) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (serviceExist != null)
            {
                throw new BadHttpRequestException(MessageConstant.ServiceMessage.ServiceExisted);
            }

            Repository.Entity.Service service = new Repository.Entity.Service()
            {
                Id = Guid.NewGuid(),
                ServiceName = createNewServiceRequest.ServiceName,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Repository.Entity.Service>().InsertAsync(service);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewServiceResponse createNewServiceResponse = null;

            if (isSuccessfully)
            {
                createNewServiceResponse = new CreateNewServiceResponse()
                {
                    ServiceId = service.Id,
                    ServiceName = service.ServiceName,
                };
            }
            return createNewServiceResponse;
        }

        public async Task<bool> DeleteService(Guid id)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                throw new BadHttpRequestException(MessageConstant.ServiceMessage.ServiceNotExist);
            }

            service.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            service.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Repository.Entity.Service>().UpdateAsync(service);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<IPaginate<GetServiceResponse>> GetAllServices(int page, int size)
        {
            var services = await _unitOfWork.GetRepository<Repository.Entity.Service>().GetPagingListAsync(
                selector: s => new GetServiceResponse()
                {
                    ServiceId = s.Id,
                    ServiceName = s.ServiceName,
                },
                predicate: s => s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                size: size,
                page: page);
            return services;
        }

        public async Task<GetServiceResponse> GetService(Guid id)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                selector: s => new GetServiceResponse()
                {
                    ServiceId = s.Id,
                    ServiceName = s.ServiceName,
                },
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                throw new BadHttpRequestException(MessageConstant.ServiceMessage.ServiceNotExist);
            }
            return service;
        }

        public async Task<bool> UpdateService(Guid id, UpdateServiceRequest updateServiceRequest)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                throw new BadHttpRequestException(MessageConstant.ServiceMessage.ServiceNotExist);
            }

            service.ServiceName = string.IsNullOrEmpty(updateServiceRequest.ServiceName) ? service.ServiceName : updateServiceRequest.ServiceName;
            service.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Repository.Entity.Service>().UpdateAsync(service);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }
    }
}
