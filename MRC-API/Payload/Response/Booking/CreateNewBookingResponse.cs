﻿namespace MRC_API.Payload.Response.Booking
{
    public class CreateNewBookingResponse
    {
        public Guid BookingId { get; set; }
        public DateTime? BookingDate { get; set; }
        public string Status { get; set; }
        public string ServiceName { get; set; }
        public string Title {  get; set; }    
    }
}
