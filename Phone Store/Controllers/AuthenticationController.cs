using Application.CQRS.AuthenticateCQRS.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Phone_Store.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        public async Task<IActionResult> Post(LoginCommand loginCommand)
        {
            var login = await _mediator.Send(loginCommand);
            return Ok(login);

        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var register = await _mediator.Send(registerCommand);
            return Ok(register);

        }
    }
}
