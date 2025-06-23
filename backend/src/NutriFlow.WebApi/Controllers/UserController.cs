using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NutriFlow.Application.Requests.Users;
using NutriFlow.Domain.Requests.Users;

namespace NutriFlow.WebApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AddUserRequest request)
        {
            Result result = await _mediator.Send(request);

            if (result.IsFailed)
                return BadRequest(new
                {
                    Errors = result.Errors.Select(e => e.Message)
                });

            return Ok(new { Message = "Usuário criado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result = await _mediator.Send(request);

            if (result.IsFailed)
                return Unauthorized(new { Errors = result.Errors.Select(e => e.Message) });

            return Ok(new { Token = result.Value });
        }
    }
}
