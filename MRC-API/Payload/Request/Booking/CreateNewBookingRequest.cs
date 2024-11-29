namespace MRC_API.Payload.Request.Booking
{
    public class CreateNewBookingRequest
    {
            
        public Guid? ServiceId { get; set; }       // Reference to the service being booked (optional)
        public DateTime? BookingDate { get; set; }
        public string Content { get; set; }
        public string Tile {  get; set; }
    }
}
