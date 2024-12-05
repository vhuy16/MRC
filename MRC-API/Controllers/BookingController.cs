using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response;
using MRC_API.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using MRC_API.Infrastructure;

namespace MRC_API.Controllers
{   
    [ApiController]
    [Route(ApiEndPointConstant.Booking.BookingEndPoint)]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService)
        {
            _logger = logger;
            _bookingService = bookingService;
        }
        [HttpPost(ApiEndPointConstant.Booking.CreateNewBooking)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewBooking([FromBody] CreateNewBookingRequest createNewBookingRequest)
        {
            var response = await _bookingService.CreateNewBooking(createNewBookingRequest);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.Booking.GetAllBookings)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllBookings([FromQuery] int? page, [FromQuery] int? size, [FromQuery] bool? isAscending = null)
        {
            var response = await _bookingService.GetAllBookings(page ?? 1, size ?? 10, isAscending);
            return StatusCode(int.Parse(response.status), response);
        }

        [CustomAuthorize(roles: "Admin,Manager")]
        [HttpGet(ApiEndPointConstant.Booking.GetBookings)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetBookings([FromQuery] int? page, [FromQuery] int? size, [FromQuery] bool? isAscending = null, [FromQuery] string? status =null)
        {
            var response = await _bookingService.GetBookings(page ?? 1, size ?? 10, isAscending, status);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.Booking.GetBooking)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetBooking([FromRoute] Guid id)
        {
            var response = await _bookingService.GetBooking(id);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpGet(ApiEndPointConstant.Booking.GetbookingByStatus)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetBookingByStatus([FromQuery] string status,
                                                            [FromQuery] int? page, 
                                                           [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _bookingService.GetBookingByStatus( pageNumber, pageSize, status);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpPut(ApiEndPointConstant.Booking.UpdateBooking)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, [FromBody] UpdateBookingRequest updateBookingRequest)
        {
            var response = await _bookingService.UpdateBooking(id, updateBookingRequest);
            return StatusCode(int.Parse(response.status), response);
        }

        [HttpDelete(ApiEndPointConstant.Booking.DeleteBooking)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var response = await _bookingService.DeleteBooking(id);
            return StatusCode(int.Parse(response.status), response);
        }
    }
}
