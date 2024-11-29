namespace MRC_API.Payload.Response.Booking
{
    public class GetBookingResponse
    {
        public Guid Id { get; set; }

        public Guid? ServiceId { get; set; } 
        public string ServiceName { get; set;}
        public string Content { get; set; }
        public string Tile { get; set; }
        public DateTime? BookingDate { get; set; }  // The date and time of the booking
        public string Status { get; set; }         // Status of the booking (e.g., confirmed, cancelled)
        public DateTime? InsDate { get; set; }      // The date when the booking was created
        public DateTime? UpDate { get; set; }
    }
}
