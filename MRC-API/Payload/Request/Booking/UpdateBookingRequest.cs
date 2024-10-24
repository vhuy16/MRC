namespace MRC_API.Payload.Request.Booking
{
    public class UpdateBookingRequest
    {
        public Guid? ServiceId { get; set; }      // Reference to the service being booked (optional)
        public DateTime? BookingDate { get; set; }
        public string Content { get; set; }// The new date and time of the booking (optional)
        public string Status { get; set; }
    }
}
