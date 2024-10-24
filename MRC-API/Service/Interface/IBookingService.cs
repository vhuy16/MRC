using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response.Booking;
using Repository.Paginate;

namespace MRC_API.Service.Interface
{
    public interface IBookingService
    {

        Task<CreateNewBookingResponse> CreateNewBooking(CreateNewBookingRequest request); 
        Task<IPaginate<GetBookingResponse>> GetAllBookings(int page, int size); 
        Task<GetBookingResponse> GetBooking(Guid id); 
        Task<bool> UpdateBooking(Guid id, UpdateBookingRequest request); 
        Task<bool> DeleteBooking(Guid id);
    }
}
