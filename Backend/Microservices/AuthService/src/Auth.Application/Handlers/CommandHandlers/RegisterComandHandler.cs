using MediatR;
using UserService.DAL.Entities;
using UserService.BLL.Exceptions;
using UserService.BLL.DTOs.Identity;
using UserService.BLL.Commands;
using UserService.BLL.Queries;
using Library.Application.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace UserService.BLL.Handlers.CommandHandlers
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
    {
        private readonly IMediator _mediator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<UserEntity> _userManager;

        public RegisterCommandHandler(IMediator mediator, IUnitOfWork unitOfWork, UserManager<UserEntity> userManager)
        {
            _mediator = mediator;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = CreateRegisterUser(request.RegisterRequest);
            var result = await _unitOfWork.UserManagers.CreateUserAsync(user, request.RegisterRequest.Password);
            var createdUser = await _unitOfWork.UserManagers.FindByEmailAsync(request.RegisterRequest.Email);

            await _unitOfWork.UserManagers.AddToRoleAsync(createdUser);
            var authQuery = new AuthenticateQuery
            {
                AuthRequest = new AuthRequest
                {
                    Email = request.RegisterRequest.Email,
                    Password = request.RegisterRequest.Password
                }
            };
            return await _mediator.Send(authQuery, cancellationToken);
        }

        private UserEntity CreateRegisterUser(RegisterRequest request)
        {
            return new UserEntity
            {
                Name = request.Nickname,
                Email = request.Email,
                UserName = request.Email
            };
        }

    }
}