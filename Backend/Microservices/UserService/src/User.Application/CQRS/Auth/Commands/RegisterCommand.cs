using UserService.BLL.DTOs.Identity;
using MediatR;

namespace User.BLL.CQRS.Auth.Commands
{
    public class RegisterCommand : IRequest<AuthResponse>
    {
        public RegisterRequest RegisterRequest { get; set; }
    }
} 