using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response;
using MRC_API.Payload.Response.Booking;
using Repository.Paginate;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MRC_API.Service.Interface
{
    public interface IBookingService
    {
        Task<ApiResponse> CreateNewBooking(CreateNewBookingRequest request);
        Task<ApiResponse> GetAllBookings(int page, int size, bool? isAscending);
        Task<ApiResponse> GetBooking(Guid id);
        Task<ApiResponse> UpdateBooking(Guid id, UpdateBookingRequest request);
        Task<ApiResponse> DeleteBooking(Guid id);
        Task<ApiResponse> GetBookingByStatus(int page, int size, string status);
    }
}
