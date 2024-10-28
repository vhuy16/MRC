using AutoMapper;
using Bean_Mind.API.Utils;
using Business.Interface;
using MRC_API.Constant;
using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response.Booking;
using MRC_API.Service.Interface;
using MRC_API.Utils;
using Repository.Entity;
using Repository.Enum;
using Repository.Paginate;

namespace MRC_API.Service.Implement
{
    public class BookingService : BaseService<Booking>, IBookingService
    {
        public BookingService(IUnitOfWork<MrcContext> unitOfWork, ILogger<Booking> logger, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            : base(unitOfWork, logger, mapper, httpContextAccessor)
        {
        }

        public async Task<CreateNewBookingResponse> CreateNewBooking(CreateNewBookingRequest createNewBookingRequest)
        {
            Guid? userId = UserUtil.GetAccountId(_httpContextAccessor.HttpContext);
            if (userId == null)
            {
                throw new BadHttpRequestException("User ID cannot be null.");
            }

            var bookingExists = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.UserId.Equals(userId)
                                && b.BookingDate.Equals(createNewBookingRequest.BookingDate));

            if (bookingExists != null)
            {
                throw new BadHttpRequestException(MessageConstant.BookingMessage.BookingExists);
            }

            Booking booking = new Booking()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                ServiceId = createNewBookingRequest.ServiceId,
                BookingDate = createNewBookingRequest.BookingDate,
                Content = createNewBookingRequest.Content,
                InsDate = TimeUtils.GetCurrentSEATime(),
                UpDate = TimeUtils.GetCurrentSEATime(),
                Status = StatusEnum.Confirmed.GetDescriptionFromEnum()
            };

            await _unitOfWork.GetRepository<Booking>().InsertAsync(booking);
            bool isSuccessfully = await _unitOfWork.CommitAsync() > 0;
            CreateNewBookingResponse createNewBookingResponse = null;

            if (isSuccessfully)
            {
                createNewBookingResponse = new CreateNewBookingResponse()
                {
                    BookingId = booking.Id,
                    BookingDate = booking.BookingDate,
                    Status = booking.Status
                };
            }

            return createNewBookingResponse;
        }

        public async Task<bool> DeleteBooking(Guid id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));
            if (booking == null)
            {
                throw new BadHttpRequestException(MessageConstant.BookingMessage.BookingNotExist);
            }

            booking.Status = StatusEnum.Cancelled.GetDescriptionFromEnum();
            booking.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

        public async Task<IPaginate<GetBookingResponse>> GetAllBookings(int page, int size)
        {
            var bookings = await _unitOfWork.GetRepository<Booking>().GetPagingListAsync(
                selector: b => new GetBookingResponse()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
               
                size: size,
                page: page);

            return bookings;
        }
        public async Task<List<GetBookingResponse>> GetBookingByStatus(string status)
        {
            var listBooking = await _unitOfWork.GetRepository<Booking>().GetListAsync(
                selector: b => new GetBookingResponse()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
                predicate: p => p.Status.Equals(status));
            if (listBooking == null)
            {
                throw new BadHttpRequestException(MessageConstant.BookingMessage.BookingNotExist);
            }
            return (List<GetBookingResponse>)listBooking;
        }
        public async Task<GetBookingResponse> GetBooking(Guid id)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                selector: b => new GetBookingResponse()
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    ServiceId = b.ServiceId,
                    BookingDate = b.BookingDate,
                    Status = b.Status,
                },
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));

            if (booking == null)
            {
                throw new BadHttpRequestException(MessageConstant.BookingMessage.BookingNotExist);
            }

            return booking;
        }

        public async Task<bool> UpdateBooking(Guid id, UpdateBookingRequest updateBookingRequest)
        {
            var booking = await _unitOfWork.GetRepository<Booking>().SingleOrDefaultAsync(
                predicate: b => b.Id.Equals(id) && b.Status.Equals(StatusEnum.Confirmed.GetDescriptionFromEnum()));
            if (booking == null)
            {
                throw new BadHttpRequestException(MessageConstant.BookingMessage.BookingNotExist);
            }
            booking.Status = updateBookingRequest.Status ?? booking.Status;          
            booking.BookingDate = updateBookingRequest.BookingDate ?? booking.BookingDate;
            booking.Content = updateBookingRequest.Content ?? booking.Content;
            booking.UpDate = TimeUtils.GetCurrentSEATime();
            _unitOfWork.GetRepository<Booking>().UpdateAsync(booking);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            return isSuccessful;
        }

    }
}
