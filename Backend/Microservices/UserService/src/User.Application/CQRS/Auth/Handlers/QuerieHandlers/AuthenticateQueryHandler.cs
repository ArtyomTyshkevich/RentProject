using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using UserService.DAL.Entities;
using Library.Application.Interfaces;
using UserService.BLL.Exceptions;
using UserService.BLL.Extensions;
using UserService.BLL.DTOs.Identity;
using User.BLL.CQRS.Auth.Queries;
using User.BLL.CQRS.Auth.Commands;

namespace User.BLL.CQRS.Auth.Handlers.QuerieHandlers
{
    public class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, AuthResponse>
    {
        private readonly IMediator _mediator;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthenticateQueryHandler(IMediator mediator, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mediator = mediator;
            _configuration = configuration;
        }

        public async Task<AuthResponse> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            var user = await ValidateUserCredentials(request.AuthRequest, cancellationToken);
            var roles = await _unitOfWork.Users.GetRolesAsync(user, cancellationToken); ;
            var createTokenCommand = new CreateTokenCommand { User = user, Roles = roles };
            var accessToken = await _mediator.Send(createTokenCommand, cancellationToken);
            UpdateUserTokenAndExpiry(user);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new AuthResponse
            {
                Username = user.UserName!,
                Email = user.Email!,
                Token = accessToken,
                RefreshToken = user.RefreshToken!
            };
        }

        private async Task<UserEntity> ValidateUserCredentials(AuthRequest request, CancellationToken cancellationToken)
        {
            var managedUser = await _unitOfWork.UserManagers.FindByEmailAsync(request.Email);
            if (managedUser == null || !await _unitOfWork.UserManagers.CheckPasswordAsync(managedUser, request.Password))
            {
                throw new BadCredentialsException();
            }
            var user = await _unitOfWork.Users.GetByEmailAsync(request.Email, cancellationToken);
            if (user is null) throw new UserNotFoundException(request.Email);
            return user;
        }

        private void UpdateUserTokenAndExpiry(UserEntity user)
        {
            user.RefreshToken = _configuration.GenerateRefreshToken();
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_configuration.GetSection("Jwt:RefreshTokenValidityInDays").Get<int>());
        }

    }
}