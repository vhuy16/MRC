using Newtonsoft.Json;

namespace MRC_API.Payload.Response
{
    public class ApiResponse
    {
        public string status { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? message { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? WarnMessage { get; set; }
        public object? data { get; set; }
    }
}
