using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using UserService.DAL.Entities;
using UserService.BLL.Extensions;
using Library.Application.Interfaces;
using UserService.BLL.Exceptions;
using UserService.BLL.DTOs.Identity;
using UserService.BLL.Commands;

namespace UserService.BLL.Handlers.CommandHandlers
{
    public class RefreshTokenComandHandler : IRequestHandler<RefreshTokenCommand, TokenModel>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenComandHandler(IMediator mediator, UserManager<UserEntity> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<TokenModel> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var principal = _configuration.GetPrincipalFromExpiredToken(request.tokenModel.AccessToken);
            if (principal == null) throw new TokenInvalidException();
            var user = await GetUserAndValidateRefreshToken(principal, request.tokenModel.RefreshToken!, cancellationToken);
            var newAccessToken = _configuration.CreateToken(principal.Claims.ToList());
            var newRefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshToken = newRefreshToken;
            await _unitOfWork.UserManagers.UpdateUserAsync(user);

            return new TokenModel { AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken), RefreshToken = newRefreshToken };
        }

        private async Task<UserEntity> GetUserAndValidateRefreshToken(ClaimsPrincipal principal, string refreshToken, CancellationToken cancellationToken)
        {
            var username = principal.Identity!.Name;
            var user = await _unitOfWork.UserManagers.FindByNameAsync(username!);
            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new RefreshTokenInvalidException();
            }
            return user;
        }

    }
}