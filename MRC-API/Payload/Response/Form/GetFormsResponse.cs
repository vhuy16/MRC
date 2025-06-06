﻿namespace MRC_API.Payload.Response.Form
{
    public class GetFormsResponse
    {
        public Guid? Id { get; set; }
        public string? CompanyName { get; set; }
        public string? Email { get; set; }
        public string? ServiceType { get; set; }
        public string? Question { get; set; }
        public DateTime? DateSent { get; set; }
    }
}
