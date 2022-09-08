using Application.Features.Auth.Commands.AuthLogin;
using Application.Features.Auth.Commands.AuthRegister;
using Core.Security.JWT;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : BaseController
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterAuthCommand registerAuthCommand)
        {
            AccessToken register = await Mediator.Send(registerAuthCommand);
            return Created("", register);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginAuthCommand loginAuthCommand)
        {
            AccessToken login = await Mediator.Send(loginAuthCommand);
            return Ok(login);
        }
    }
}
