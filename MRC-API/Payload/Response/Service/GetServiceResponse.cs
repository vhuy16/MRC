namespace MRC_API.Payload.Response.Service
{
    public class GetServiceResponse
    {
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }

        public string Status { get; set; } = null!;

        public DateTime? InsDate { get; set; }

        public DateTime? UpDate { get; set; }

        public DateTime? DeleteAt { get; set; }
    }
}
