using Core.Interfaces;
using Core.Models;
using Domain.Handlers.Command;
using Domain.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Domain.Handlers
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResultCommand<TokenInfo>>
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public LoginHandler(
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<ResultCommand<TokenInfo>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var appUser = await _userRepository.GetUserByNameAsync(request.LoginData.UserName);

            if (appUser == null)
            {
                return new ResultCommand<TokenInfo>
                {
                    Success = false,
                    Message = "The user doesn't exist..."
                };
            }

            if (!await _userRepository.CheckUserPasswordAsync(appUser, request.LoginData.Password))
            {
                return new ResultCommand<TokenInfo>
                {
                    Success = false,
                    Message = "Incorrect password..."
                };
            }

            var tokenInfo = await _tokenService.GenerateTokenAsync(appUser);

            return new ResultCommand<TokenInfo>
            {
                Success = true,
                Result = tokenInfo
            };
        }
    }
}
