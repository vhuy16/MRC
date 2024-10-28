using Microsoft.AspNetCore.Mvc;
using MRC_API.Constant;
using MRC_API.Payload.Request.Booking;
using MRC_API.Payload.Response.Booking;
using MRC_API.Service.Interface;
using Repository.Paginate;

namespace MRC_API.Controllers
{
    [ApiController]
    [Route(ApiEndPointConstant.Booking.BookingEndPoint)] // Adjust endpoint based on your routing structure
    public class BookingController : BaseController<BookingController>
    {
        private readonly IBookingService _bookingService;

        public BookingController(ILogger<BookingController> logger, IBookingService bookingService) : base(logger)
        {
            _bookingService = bookingService;
        }

        [HttpPost(ApiEndPointConstant.Booking.CreateNewBooking)]
        [ProducesResponseType(typeof(CreateNewBookingResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> CreateNewBooking([FromBody] CreateNewBookingRequest createNewBookingRequest)
        {
            var createNewBookingResponse = await _bookingService.CreateNewBooking(createNewBookingRequest);
            if (createNewBookingResponse == null)
            {
                return Problem(MessageConstant.BookingMessage.CreateBookingFail);
            }
            return CreatedAtAction(nameof(GetBooking), new { id = createNewBookingResponse.BookingId }, createNewBookingResponse);
        }

        [HttpGet(ApiEndPointConstant.Booking.GetAllBookings)]
        [ProducesResponseType(typeof(IPaginate<GetBookingResponse>), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllBookings([FromQuery] int? page, [FromQuery] int? size)
        {
            int pageNumber = page ?? 1;
            int pageSize = size ?? 10;
            var response = await _bookingService.GetAllBookings(pageNumber, pageSize);
            if (response == null)
            {
                return Problem(MessageConstant.BookingMessage.BookingIsEmpty);
            }
            return Ok(response);
        }

        [HttpGet(ApiEndPointConstant.Booking.GetBooking)]
        [ProducesResponseType(typeof(GetBookingResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetBooking([FromRoute] Guid id)
        {
            var response = await _bookingService.GetBooking(id);
            if (response == null)
            {
                return Problem(MessageConstant.BookingMessage.BookingIsEmpty);
            }
            return Ok(response);
        }
        [HttpGet(ApiEndPointConstant.Booking.GetbookingByStatus)]
        [ProducesResponseType(typeof(GetBookingResponse), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> GetbookingByStatus( string status)
        {
            var response = await _bookingService.GetBookingByStatus(status);
            if (response == null)
            {
                return Problem(MessageConstant.BookingMessage.BookingIsEmpty);
            }
            return Ok(response);
        }

        [HttpPut(ApiEndPointConstant.Booking.UpdateBooking)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> UpdateBooking([FromRoute] Guid id, [FromBody] UpdateBookingRequest updateBookingRequest)
        {
            var response = await _bookingService.UpdateBooking(id, updateBookingRequest);
            return Ok(response);
        }

        [HttpDelete(ApiEndPointConstant.Booking.DeleteBooking)]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesErrorResponseType(typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteBooking([FromRoute] Guid id)
        {
            var response = await _bookingService.DeleteBooking(id);
            return Ok(response);
        }
    }
}
