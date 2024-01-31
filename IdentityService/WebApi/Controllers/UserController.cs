using WebApi.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MediatR;
using Domain.Handlers.Queries;
using AutoMapper;
using Core.Models.Entities;
using Domain.Handlers.Command;
using Microsoft.AspNetCore.Identity;
using Domain.Handlers.Commands;
using Core.ViewModels;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public UserController(
            ILogger<UserController> logger,
            IMediator mediator,
            IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<User>> GetUsers(CancellationToken cancellationToken)
        {
            var appUsers = await _mediator.Send(new GetAllUsersQuery(), cancellationToken);

            return _mapper.Map<IEnumerable<User>>(appUsers);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var appUser = _mapper.Map<AppUser>(request.User);
            var result = await _mediator.Send(new CreateUserCommand { User = appUser, Password = request.Password, Roles = request.Roles }, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> DeleteUserById(DeleteUserByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteUserByIdCommand { UserId = request.UserId }, cancellationToken);

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IdentityResult>> AddUserToRolesById(AddUserToRolesByIdRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new AddUserToRolesCommand { UserId = request.UserId, Roles = request.Roles }, cancellationToken);

            return Ok(result);
        }
    }
}