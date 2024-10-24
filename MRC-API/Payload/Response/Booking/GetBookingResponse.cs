namespace MRC_API.Payload.Response.Booking
{
    public class GetBookingResponse
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }           // Reference to the user who made the booking
        public Guid? ServiceId { get; set; }       // Reference to the service booked (optional)
        public DateTime BookingDate { get; set; }  // The date and time of the booking
        public string Status { get; set; }         // Status of the booking (e.g., confirmed, cancelled)
        public DateTime InsDate { get; set; }      // The date when the booking was created
        public DateTime UpDate { get; set; }
    }
}
