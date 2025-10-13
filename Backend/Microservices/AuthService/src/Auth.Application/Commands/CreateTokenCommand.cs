using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.DAL.Entities;

namespace UserService.BLL.Commands
{
    public class CreateTokenCommand : IRequest<string>
    {
        public User User { get; set; }
        public List<IdentityRole<Guid>> Roles { get; set; }
    }
}