using UserService.BLL.DTOs.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace UserService.BLL.Commands
{
    public class RefreshTokenCommand : IRequest<TokenModel>
    {
        public TokenModel tokenModel { get; set; }
        public List<IdentityRole<long>> Roles { get; set; }
    }
}