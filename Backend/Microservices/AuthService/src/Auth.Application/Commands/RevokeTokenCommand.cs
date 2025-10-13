using MediatR;

namespace UserService.BLL.Commands
{
    public class RevokeTokenCommand : IRequest<Unit>
    {
        public string Username { get; set; }
    }

    public class RevokeAllTokensCommand : IRequest<Unit>
    {
    }
}