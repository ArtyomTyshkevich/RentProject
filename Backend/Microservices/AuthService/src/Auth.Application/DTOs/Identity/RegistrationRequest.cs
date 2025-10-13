using System.ComponentModel.DataAnnotations;

namespace UserService.BLL.DTOs.Identity
{
    public class RegistrationRequest
    {
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}