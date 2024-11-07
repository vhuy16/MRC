using Repository.Enum;

namespace MRC_API.Payload.Response.User
{
    public class CreateNewAccountResponse
    {
        public Guid? Id { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? PhoneNumber { get; set; }

    }
}