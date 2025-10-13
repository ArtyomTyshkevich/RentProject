using UserService.BLL.DTOs.Identity;
using MediatR;

namespace UserService.BLL.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public RegisterRequest RegisterRequest { get; set; }
    }
} 