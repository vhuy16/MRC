using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Booking;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;



namespace MRC_API.Service.Implement
{
    public class BookingService : BaseService<Booking>, IBookingService
    {
        public BookingService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Booking> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<ApiResponse> CreateNewBooking(CreateNewBookingRequest createNewBookingRequest)
        {
           
        
            var service = await _unitOfWork.GetRepository<Repository.Entity.Service>().SingleOrDefaultAsync(
                predicate: p => p.Id.Equals(createNewBookingRequest.ServiceId));
            if (service == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status400BadRequest.ToString(),
                    message = "Service ID is not exits",
                    data = null
                };
            }
            var bookingExists = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.BookingDate.Equals(createNewBookingRequest.BookingDate));

         

            Booking booking = new Booking
            {
                Id = Guid.NewGuid(),
                
                ServiceId = createNewBookingRequest.ServiceId,
                BookingDate = createNewBookingRequest.BookingDate,
                Content = createNewBookingRequest.Content,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Available.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Booking>().InsertAsync(booking);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to create booking.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status201Created.ToString(),
                message = "Booking created successfully.",
                data = new CreateNewBookingResponse
                {
                    BookingId = booking.Id,
                    BookingDate = booking.BookingDate,
                    Status = booking.Status
                }
            };
        }

        public async Task<ApiResponse> DeleteBooking(Guid id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));

            if (booking == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.BookingMessage.BookingNotExist,
                    data = null
                };
            }

            booking.Status = StatusEnum.Cancelled.GetDescriptionFromEnum();
            booking.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to delete booking.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Booking deleted successfully.",
                data = true
            };
        }

        public async Task<ApiResponse> GetAllBookings(int page, int size, bool? isAscending)
        {
            var bookings = await _unitOfWork.GetRepository<Booking>().GetPagingListAsync(
                selector: b => new GetBookingResponse
                {
                    Id = b.Id,         
                    ServiceName = b.Service.ServiceName,
                    content = b.Content,
                    InsDate = b.InsDate,
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
                 predicate: p => p.Status.Equals(StatusEnum.Available.GetDescriptionFromEnum()),
                 orderBy: q => isAscending.HasValue
            ? (isAscending.Value ? q.OrderBy(p => p.InsDate) : q.OrderByDescending(p => p.InsDate)) // Sắp xếp theo ngày tạo
            : q.OrderByDescending(p => p.InsDate),
            size: size,
                page: page);

            int totalItems = bookings.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (bookings == null || bookings.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Booking retrieved successfully.",
                    data = new Paginate<Booking>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Booking>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Bookings retrieved successfully.",
                data = bookings
            };
        }

        public async Task<ApiResponse> GetBookingByStatus(int page, int size, string status)
        {
            var listBooking = await _unitOfWork.GetRepository<Booking>().GetPagingListAsync(
                selector: b => new GetBookingResponse
                {
                    Id = b.Id,
                    
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
                predicate: p => p.Status.Equals(status),
                page: page,
            size: size

                );
            int totalItems = listBooking.Total;
            int totalPages = (int)Math.Ceiling((double)totalItems / size);
            if (listBooking == null || listBooking.Items.Count == 0)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status200OK.ToString(),
                    message = "Booking retrieved successfully.",
                    data = new Paginate<Booking>()
                    {
                        Page = page,
                        Size = size,
                        Total = totalItems,
                        TotalPages = totalPages,
                        Items = new List<Booking>()
                    }
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Bookings retrieved successfully.",
                data = listBooking
            };
        }

        public async Task<ApiResponse> GetBooking(Guid id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                selector: b => new GetBookingResponse
                {
                    Id = b.Id,
                   
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));

            if (booking == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.BookingMessage.BookingNotExist,
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Booking retrieved successfully.",
                data = booking
            };
        }

        public async Task<ApiResponse> UpdateBooking(Guid id, UpdateBookingRequest updateBookingRequest)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));

            if (booking == null)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status404NotFound.ToString(),
                    message = MessageConstant.BookingMessage.BookingNotExist,
                    data = null
                };
            }

            booking.Status = updateBookingRequest.Status ?? booking.Status;
            booking.BookingDate = updateBookingRequest.BookingDate ?? booking.BookingDate;
            booking.Content = updateBookingRequest.Content ?? booking.Content;
            booking.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;

            if (!isSuccessful)
            {
                return new ApiResponse
                {
                    status = StatusCodes.Status500InternalServerError.ToString(),
                    message = "Failed to update booking.",
                    data = null
                };
            }

            return new ApiResponse
            {
                status = StatusCodes.Status200OK.ToString(),
                message = "Booking updated successfully.",
                data = true
            };
        }
    }
}