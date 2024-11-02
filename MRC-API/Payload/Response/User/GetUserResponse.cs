using Repository.Enum;

namespace MRC_API.Payload.Response.User
{
    public class GetUserResponse
    {
        public Guid? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public string? Role {  get; set; }
    }
}
