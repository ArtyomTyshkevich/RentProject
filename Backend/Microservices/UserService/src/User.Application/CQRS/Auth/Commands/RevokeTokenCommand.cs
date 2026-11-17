using MediatR;

namespace User.BLL.CQRS.Auth.Commands
{
    public class RevokeTokenCommand : IRequest<Unit>
    {
        public string Username { get; set; }
    }

    public class RevokeAllTokensCommand : IRequest<Unit>
    {
    }
}