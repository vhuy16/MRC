namespace MRC_API.Payload.Request.Form
{
    public class CreateFormRequest
    {
        public string CompanyName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string ServiceType { get; set; } = null!;
        public string Question { get; set; } = null!;
    }
}
