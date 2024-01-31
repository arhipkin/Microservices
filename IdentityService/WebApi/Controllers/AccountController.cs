using AutoMapper;
using Core.Interfaces;
using Core.Models;
using Core.Models.Entities;
using Core.ViewModels;
using Domain.Handlers.Command;
using Domain.Handlers.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public AccountController(
                ITokenService tokenService,
                IMediator mediator,
                IMapper mapper
            )
        {
            _tokenService = tokenService;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResultResponse<TokenInfo>>> Login(LoginData loginData)
        {
            var result = await _mediator.Send(new LoginCommand { LoginData = loginData });
            var response = _mapper.Map<ResultResponse<TokenInfo>>(result);

            return Ok(response);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<IdentityResult>> Register(RegistrationRequest registrationRequest)
        {
            var appUser = _mapper.Map<AppUser>(registrationRequest.User);

            var result = await _mediator.Send(new CreateUserCommand { User = appUser, Password = registrationRequest.Password, Roles = new[] { "user" } });

            return Ok(result);
        }
    }
}
