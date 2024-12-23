﻿using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Service;
using MRC_API.Payload.Response.Service;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System;
using System.Threading.Tasks;
using MRC_API.Payload.Response;


namespace MRC_API.Service.Implement
{
    public class ServiceService : BaseService<Repository.Entity.Service>, IServiceService
    {
        public ServiceService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Repository.Entity.Service> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateNewService(CreateNewServiceRequest createNewServiceRequest)
        {
            var serviceExist = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.ServiceName.Equals(createNewServiceRequest.ServiceName) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (serviceExist != null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = MessageConstant.ServiceMessage.ServiceExisted,
                    data = null
                };
            }

            var service = new Repository.Entity.Service()
            {
                Id = Guid.NewGuid(),
                ServiceName = createNewServiceRequest.ServiceName,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Repository.Entity.Service>().InsertAsync(service);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to create service.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status201Created.ToString(),
                message = "Service created successfully.",
                data = new CreateNewServiceResponse()
                {
                    ServiceId = service.Id,
                    ServiceName = service.ServiceName,
                }
            };
        }

        public async Task<ApiResponse> DeleteService(Guid id)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ServiceMessage.ServiceNotExist,
                    data = null
                };
            }

            service.Status = StatusEnum.Unavailable.GetDescriptionFromEnum();
            service.UpDate = TimeUtils.GetCurrentSEATime();
            service.DeleteAt = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Repository.Entity.Service>().UpdateAsync(service);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to delete service.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Service deleted successfully.",
                data = true
            };
        }

        public async Task<ApiResponse> GetAllServices(int page, int size, string? searchName, bool? isAscending)
        {
            var services = await _unitOfWork.GetRepository<Repository.Entity.Service>().GetPagingListAsync(
                selector: s => new GetServiceResponse()
                {
                    ServiceId = s.Id,
                    ServiceName = s.ServiceName,
                    DeleteAt = s.DeleteAt,
                    InsDate = s.InsDate,
                    UpDate = s.UpDate,
                    Status = s.Status
                },
               predicate: p => p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()) &&
                                (string.IsNullOrEmpty(searchName) || p.ServiceName.Contains(searchName)),
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.ServiceName) : q.OrderByDescending(p => p.ServiceName))
                    : q.OrderByDescending(p => p.InsDate),
                size: size,
                page: page);
            int totalItems = services.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (services == null || services.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Service retrieved successfully.",
                    data = new Paginate<Repository.Entity.Service>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Repository.Entity.Service>()
                    }
                };
            }
            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Services retrieved successfully.",
                data = services
            };
        }
        public async Task<ApiResponse> GetAllServicesByStatus(int page, int size, string? searchName, string status, bool? isAscending)
        {
            var services = await _unitOfWork.GetRepository<Repository.Entity.Service>().GetPagingListAsync(
                selector: s => new GetServiceResponse()
                {
                    ServiceId = s.Id,
                    ServiceName = s.ServiceName,
                    Status = s.Status,
                    DeleteAt = s.DeleteAt,
                    InsDate = s.InsDate,
                    UpDate = s.UpDate,
                },
               predicate: p =>  (string.IsNullOrEmpty(searchName) || p.ServiceName.Contains(searchName))
                                && (string.IsNullOrEmpty(status) || p.Status.Equals(status)),
                orderBy: q => isAscending.HasValue
                    ? (isAscending.Value ? q.OrderBy(p => p.ServiceName) : q.OrderByDescending(p => p.ServiceName))
                    : q.OrderByDescending(p => p.InsDate),
                size: size,
                page: page);
            int totalItems = services.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (services == null || services.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Service retrieved successfully.",
                    data = new Paginate<Repository.Entity.Service>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Repository.Entity.Service>()
                    }
                };
            }
            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Services retrieved successfully.",
                data = services
            };
        }

        public async Task<ApiResponse> GetService(Guid id)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                selector: s => new GetServiceResponse()
                {
                    ServiceId = s.Id,
                    ServiceName = s.ServiceName,
                    DeleteAt = s.DeleteAt,
                    InsDate = s.InsDate,
                    UpDate = s.UpDate,
                    Status = s.Status
                },
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ServiceMessage.ServiceNotExist,
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Service retrieved successfully.",
                data = service
            };
        }

        public async Task<ApiResponse> UpdateService(Guid id, UpdateServiceRequest updateServiceRequest)
        {
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: s => s.Id.Equals(id) && s.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()));

            if (service == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.ServiceMessage.ServiceNotExist,
                    data = null
                };
            }

            service.ServiceName = string.IsNullOrEmpty(updateServiceRequest.ServiceName) ? service.ServiceName : updateServiceRequest.ServiceName;
            service.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Repository.Entity.Service>().UpdateAsync(service);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to update service.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Service updated successfully.",
                data = new GetServiceResponse
                {
                    ServiceId = service.Id,
                    ServiceName = service.ServiceName,
                    DeleteAt = service.DeleteAt,
                    InsDate = service.InsDate,
                    UpDate = service.UpDate,
                    Status = service.Status
                }
            };
        }
    }
}
