using Repository.Enum;

namespace MRC_API.Payload.Request.User
{
    public class UpdateUserRequest
    {
        public string? Email { get; set; }
        public GenderEnum? Gender { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
