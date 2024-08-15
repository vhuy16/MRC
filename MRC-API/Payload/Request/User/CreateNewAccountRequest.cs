using System.ComponentModel.DataAnnotations;

namespace MRC_API.Payload.Request.User
{
    public class CreateNewAccountRequest
    {
        [Required(ErrorMessage = "Username is missing")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Name is missing")]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
