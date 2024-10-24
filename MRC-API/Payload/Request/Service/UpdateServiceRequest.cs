namespace MRC_API.Payload.Request.Service
{
    public class UpdateServiceRequest
    {
        public string ServiceName { get; set; }   // Optional: New service name
        public string Description { get; set; }   // Optional: New service description
        public decimal? Price { get; set; }       // Optional: Updated price
        public bool? IsActive { get; set; }
    }
}
