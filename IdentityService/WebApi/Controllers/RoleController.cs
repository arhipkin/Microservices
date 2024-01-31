using AutoMapper;
using Core.Models.Entities;
using Core.ViewModels;
using Domain.Handlers.Commands;
using Domain.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IEnumerable<Role>> GetRoles(CancellationToken cancellationToken)
        {
            var roles = await _mediator.Send(new GetAllRolesQuery(), cancellationToken);

            return _mapper.Map<IEnumerable<Role>>(roles);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<IdentityResult>> CreateRole(Role role, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateRoleCommand { AppUserRole = new AppRole(role.Name)}, cancellationToken);

            return Ok(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult<IdentityResult>> DeleteRole(Role role, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteRoleCommand { RoleName = role.Name }, cancellationToken);

            return Ok(result);
        }
    }
}
