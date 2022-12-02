using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Task4.Application.CQRS.RegisteredUsers.Commands.CreateRegisteredUser;
using Task4.Application.CQRS.RegisteredUsers.Queries.GetAllRegisteredUsers;
using Task4.Application.CQRS.RegisteredUsers.Commands.BlockRegisteredUser;
using Task4.Application.CQRS.RegisteredUsers.Commands.DeleteRegisteredUser;
using Microsoft.AspNetCore.Authorization;
using Task4.WebApi.Models;

namespace Task4.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        private readonly IMapper _mapper;

        public UserController(IMapper mapper) => _mapper = mapper;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<RegisteredUsersVm>> GetAll()
        {
            var query = new GetAllRegisteredUsersQuery
            {
                IsOnlyActive = false
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Block([FromBody] BlockingRegisteredUserDto blockUserDto)
        {
            var command = _mapper.Map<BlockRegisteredUserCommand>(blockUserDto);
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete([FromBody] DeleteRegisteredUserDto deleteUserDto)
        {
            var command = _mapper.Map<DeleteRegisteredUserCommand>(deleteUserDto);
            await Mediator.Send(command);
            return NoContent();
        }
    }
}
